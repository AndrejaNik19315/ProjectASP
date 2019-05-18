using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(g => g.Sex)
                .HasMaxLength(24)
                .IsRequired();
            builder.Property(g => g.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(g => g.Sex)
                .IsUnique();
            builder.HasKey(g => g.Id);

            builder.HasMany(g => g.Characters)
                .WithOne(c => c.Gender)
                .HasForeignKey(c => c.GenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
