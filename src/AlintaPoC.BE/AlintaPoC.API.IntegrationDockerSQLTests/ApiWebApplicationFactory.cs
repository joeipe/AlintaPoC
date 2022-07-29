using AlintaPoC.Data;
using AlintaPoC.Domain;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlintaPoC.API.IntegrationDockerSQLTests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>, IAsyncLifetime
    {
        private readonly DockerClient _dockerClient;
        private const string ContainerImageUri = "mcr.microsoft.com/mssql/server:2022-latest";
        private string _containerId;

        string DBConnectionString = "Server=.,1434;Database=TestAlintaPoCDb_Test;User ID=sa;Password=Admin1234;"; 
        //string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=alintapocstorage;AccountKey=uDgvz0AMGPFgOzS5dLhU/2O8fR+BRtWr+MJ+TCZAa7Rub7tjYInljOZIkeUowsn/ktDcB+hChXow+AStj4Zc5A==;EndpointSuffix=core.windows.net";

        public ApiWebApplicationFactory()
        {
            _dockerClient = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
        }

        public async Task InitializeAsync()
        {
            await PullImage();

            await StartContainer();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault
                   (d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add DataContext using an in-memory database for testing.
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(DBConnectionString, builder => 
                    { 
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    })
                );

                // Build the service provider.
                var serviceProvider = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                using var scope = serviceProvider.CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<WebApplicationFactory<Startup>>>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.
                    SeedDb(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred seeding the database with test messages.Error: {ex.Message} ");
                }
            });
        }

        public async Task DisposeAsync()
        {
            if (_containerId != null)
            {
                await _dockerClient.Containers.KillContainerAsync(_containerId, new ContainerKillParameters());

                await _dockerClient.Containers.RemoveContainerAsync(_containerId, new ContainerRemoveParameters());
            }
        }

        private async Task PullImage()
        {
            await _dockerClient.Images.CreateImageAsync(new ImagesCreateParameters
            {
                FromImage = ContainerImageUri
            },
            new AuthConfig(),
            new Progress<JSONMessage>());
        }

        private async Task StartContainer()
        {
            var response = await _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = ContainerImageUri,
                AttachStderr = true,
                AttachStdin = true,
                AttachStdout = true,
                Env = new[] { "ACCEPT_EULA=Y", $"SA_PASSWORD=Admin1234" },
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    {
                        "1433", default(EmptyStruct)
                    }
                },
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        { "1433", new List<PortBinding> { new PortBinding { HostPort = "1434" } } }
                    },
                    PublishAllPorts = true
                }
            });

            _containerId = response.ID;

            await _dockerClient.Containers.StartContainerAsync(_containerId, null);
        }

        private void SeedDb(DataContext context)
        {
            if (!context.People.Any())
            {
                context.People.Add(new Person
                {
                    FirstName = "John",
                    LastName = "Doe"
                });

                context.People.Add(new Person
                {
                    FirstName = "Jane",
                    LastName = "Doe"
                });

                context.People.Add(new Person
                {
                    FirstName = "Max",
                    LastName = "Mustermann"
                });

                context.SaveChanges();
            }
        }
    }
}

