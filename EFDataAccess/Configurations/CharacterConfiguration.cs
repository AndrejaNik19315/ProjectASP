using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Configurations
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.Property(c => c.Name)
                .HasMaxLength(24)
                .IsRequired();
            builder.Property(c => c.Level)
                .HasMaxLength(80)
                .HasDefaultValue(1);
            builder.Property(c => c.Funds)
                .HasMaxLength(1000)
                .HasDefaultValue(5.0m);
            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(c => c.Name).IsUnique();
            builder.HasKey(c => c.Id);
            
        }
    }
}
