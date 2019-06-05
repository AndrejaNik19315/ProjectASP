using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.Property(i => i.MaxSlots)
                .HasMaxLength(3)
                .HasDefaultValue(20);
            builder.Property(i => i.SlotsFilled)
                .HasDefaultValue(0);
            builder.Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(i => i.Id);

            builder.HasMany(i => i.InventoryItems)
                .WithOne(ii => ii.Inventory)
                .HasForeignKey(ii => ii.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
