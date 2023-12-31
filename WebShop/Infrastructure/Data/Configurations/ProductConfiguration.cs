﻿using WebShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebShop.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity> {
    public void Configure(EntityTypeBuilder<ProductEntity> builder) {
        #region Properties

        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id).IsUnique();
        builder.Property(c => c.Title).HasMaxLength(250).IsRequired();
        builder.Property(c => c.Details).HasMaxLength(5000).IsRequired();

        // setup cascade delete of product images in db
        builder.HasMany(p => p.Images)
            .WithOne(i => i.Product)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Category)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        #endregion
    }
}