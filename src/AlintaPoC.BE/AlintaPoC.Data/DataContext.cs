using AlintaPoC.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AlintaPoC.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=TestAlintaPoCDb;Trusted_Connection=True;");
        }*/

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
