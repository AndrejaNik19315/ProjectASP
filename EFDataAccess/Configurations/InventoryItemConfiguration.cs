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

            builder.HasKey(ii => new { ii.InventoryId, ii.ItemId });
        }
    }
}
