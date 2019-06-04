using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Configurations
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
    {
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.Property(it => it.Name)
                .HasMaxLength(24)
                .IsRequired();
            builder.Property(it => it.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(it => it.Name)
                .IsUnique();

            builder.HasMany(it => it.Items)
                .WithOne(i => i.ItemType)
                .HasForeignKey(i => i.ItemTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
