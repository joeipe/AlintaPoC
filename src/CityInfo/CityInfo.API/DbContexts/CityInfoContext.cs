

using CityInfo.API.Entities;
using CityInfo.API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City()
                {
                    Id = 1,
                    Name = "Melbourne",
                    Description = "I live here",
                },
                new City()
                {
                    Id = 2,
                    Name = "Sydney",
                    Description = "I've been here"
                },
                new City()
                {
                    Id = 3,
                    Name = "Brisbane",
                    Description = "Love to live here",
                });

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest()
                {
                    Id = 1,
                    Name = "Melbourne 1",
                    Description = "Melbourne 1",
                    CityId= 1,
                },
                new PointOfInterest()
                {
                    Id = 2,
                    Name = "Melbourne 2",
                    Description = "Melbourne 2",
                    CityId = 1,
                },
                new PointOfInterest()
                {
                    Id = 3,
                    Name = "Sydney 1",
                    Description = "Sydney 1",
                    CityId = 2,
                },
                new PointOfInterest()
                {
                    Id = 4,
                    Name = "Sydney 2",
                    Description = "Sydney 2",
                    CityId = 2,
                },
                new PointOfInterest()
                {
                    Id = 5,
                    Name = "Brisbane 1",
                    Description = "Brisbane 1",
                    CityId = 3,
                },
                new PointOfInterest()
                {
                    Id = 6,
                    Name = "Brisbane 2",
                    Description = "Brisbane 2",
                    CityId = 3,
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
