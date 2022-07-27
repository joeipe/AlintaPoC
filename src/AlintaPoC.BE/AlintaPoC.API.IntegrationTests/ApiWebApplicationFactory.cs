using AlintaPoC.API.Configurations;
using AlintaPoC.Application.Services;
using AlintaPoC.Data;
using AlintaPoC.Data.Services;
using AlintaPoC.Domain;
using AlintaPoC.Integration.RedisCache;
using AlintaPoC.Integration.TableStorage.Repositories;
using Azure.Data.Tables;
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

namespace AlintaPoC.API.IntegrationTests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
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
                    options.UseInMemoryDatabase("InMemoryDbForTesting")
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
                    SeedInMemoryStore(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred seeding the database with test messages.Error: { ex.Message} ");
                }
            });
        }

        private void SeedInMemoryStore(DataContext context)
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
