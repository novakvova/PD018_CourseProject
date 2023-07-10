using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities.Catalog;

namespace WebShop.Domain.Configuration.Base {
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class {
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
        public virtual void CreateDefaultConfiguration(EntityTypeBuilder<CategoryEntity> builder) {
            // empty
        }
    }
}
