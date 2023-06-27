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

            builder.ToTable("tbl_categories");

            builder.Property(x => x.Name)
                .HasMaxLength(512)
                .IsRequired(true);

            builder.Property(x => x.Image)
                .HasMaxLength(512)
                .IsRequired(false);

            #endregion


            base.CreateDefaultConfiguration(builder);
        }
    }
}
