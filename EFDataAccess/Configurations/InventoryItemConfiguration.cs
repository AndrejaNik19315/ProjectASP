using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Configurations
{
    public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.Property(ii => ii.InventoryId)
                .IsRequired();
            builder.Property(ii => ii.ItemId)
                .IsRequired();
            builder.Property(ii => ii.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

            builder.HasKey(ii => ii.Id);

            //builder.HasOne(ii => ii.Inventory)
            //    .WithMany(i => i.InventoryItems)
            //    .HasForeignKey(i => i.InventoryId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(ii => ii.Item)
            //    .WithMany(i => i.InventoryItems)
            //    .HasForeignKey(i => i.ItemId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
