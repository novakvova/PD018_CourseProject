using WebShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebShop.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity> {
    public void Configure(EntityTypeBuilder<CategoryEntity> builder) {
        #region Properties

        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id).IsUnique();
        builder.Property(c => c.Title).HasMaxLength(250).IsRequired();
        builder.Property(c => c.Details).HasMaxLength(5000).IsRequired();
        builder.Property(c => c.Image).HasMaxLength(500);

        #endregion
    }
}
