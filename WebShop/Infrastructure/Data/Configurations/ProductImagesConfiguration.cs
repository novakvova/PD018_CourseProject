using WebShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebShop.Infrastructure.Data.Configurations;

public class ProductImagesConfiguration : IEntityTypeConfiguration<ProductImageEntity> {
    public void Configure(EntityTypeBuilder<ProductImageEntity> builder) {
        #region Properties
        builder.ToTable("ProductImages");
        #endregion
    }
}