using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Firstname)
                .HasMaxLength(24);
            builder.Property(u => u.Lastname)
                .HasMaxLength(32);
            builder.Property(u => u.Username)
                .HasMaxLength(16)
                .IsRequired();
            builder.Property(u => u.Email)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(u => u.Password)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(u => u.IsActive)
                .HasDefaultValue(0);
            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Characters)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
