using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Configurations
{
    public class ItemQualityConfiguration : IEntityTypeConfiguration<ItemQuality>
    {
        public void Configure(EntityTypeBuilder<ItemQuality> builder)
        {
            builder.Property(iq => iq.Name)
                .HasMaxLength(16)
                .IsRequired();
            builder.Property(iq => iq.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(iq => iq.Name)
                .IsUnique();

            builder.HasMany(iq => iq.Items)
                .WithOne(i => i.ItemQuality)
                .HasForeignKey(i => i.ItemQualityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
