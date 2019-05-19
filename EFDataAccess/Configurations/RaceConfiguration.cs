using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Configurations
{
    public class RaceConfiguration : IEntityTypeConfiguration<Race>
    {
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            builder.Property(r => r.Name)
                .HasMaxLength(32)
                .IsRequired();
            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.Name)
                .IsUnique();

            builder.HasMany(r => r.Characters)
                .WithOne(c => c.Race)
                .HasForeignKey(c => c.RaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
