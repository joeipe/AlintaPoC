using AlintaPoC.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Data.EntityConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(value => value.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(value => value.LastName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(value => value.DoB)
                .HasColumnType("date")
                .IsRequired();

            builder.HasOne(y => y.Role)
                .WithMany(x => x.People)
                .HasForeignKey(y => y.RoleId);
        }
    }
}
