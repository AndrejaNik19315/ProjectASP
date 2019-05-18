using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Configurations
{
    public class GameClassConfiguration : IEntityTypeConfiguration<GameClass>
    {
        public void Configure(EntityTypeBuilder<GameClass> builder)
        {
            builder.Property(gc => gc.Name)
                .HasMaxLength(32)
                .IsRequired();
            builder.Property(gc => gc.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(gc => gc.Name)
                .IsUnique();
            builder.HasKey(gc => gc.Id);

            builder.HasMany(gc => gc.Characters)
                .WithOne(c => c.GameClass)
                .HasForeignKey(c => c.GameClassId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
