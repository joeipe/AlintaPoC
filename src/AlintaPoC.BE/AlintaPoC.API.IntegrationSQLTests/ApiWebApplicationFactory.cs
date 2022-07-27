using AlintaPoC.Data;
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

namespace AlintaPoC.API.IntegrationSQLTests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        string DBConnectionString = "Server=.;Database=TestAlintaPoCDb_Test;Trusted_Connection=True;";
        //string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=alintapocstorage;AccountKey=uDgvz0AMGPFgOzS5dLhU/2O8fR+BRtWr+MJ+TCZAa7Rub7tjYInljOZIkeUowsn/ktDcB+hChXow+AStj4Zc5A==;EndpointSuffix=core.windows.net";

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
                    options.UseSqlServer(DBConnectionString)
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
                    //SeedInMemoryStore(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred seeding the database with test messages.Error: {ex.Message} ");
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            DeleteDb();

            base.Dispose(disposing);
        }

        private void DeleteDb()
        {
            var context = new DataContext(new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(DBConnectionString)
                .Options);

            context.Database.EnsureDeleted();
        }
    }
}
