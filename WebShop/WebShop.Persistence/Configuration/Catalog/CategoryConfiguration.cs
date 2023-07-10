using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Configuration.Base;
using WebShop.Domain.Entities.Catalog;

namespace WebShop.Domain.Configuration.Catalog {
    public class CategoryConfiguration : BaseConfiguration<CategoryEntity> {
        public override void Configure(EntityTypeBuilder<CategoryEntity> builder) {
            #region Properties

            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id).IsUnique();
            builder.Property(c => c.Title).HasMaxLength(250).IsRequired();
            builder.Property(c => c.Details).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.Image).HasMaxLength(500);

            #endregion


            base.CreateDefaultConfiguration(builder);
        }
    }
}
