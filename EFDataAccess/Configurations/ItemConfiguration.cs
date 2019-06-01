using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(i => i.Name)
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(i => i.Cost)
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(i => i.isCovert)
                .HasDefaultValue(0);
            builder.Property(i => i.isForSale)
                .HasDefaultValue(0);
            builder.Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(i => i.Name)
                .IsUnique();
            builder.HasKey(i => i.Id);

            builder.HasMany(i => i.Inventories)
                .WithOne(inv => inv.Item)
                .HasForeignKey(inv => inv.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
