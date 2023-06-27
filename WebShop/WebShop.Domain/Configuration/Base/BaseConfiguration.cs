using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Configuration.Base {
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class {
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
        public void CreateDefaultConfiguration(EntityTypeBuilder<TEntity> builder, string tableName = null) {
            if ( tableName != null ) {
                builder.ToTable(tableName);
            }

            builder.HasKey("Id");

            CreatePropertiesConfigurations(builder);
        }

        public void CreateDefaultConfigurationWithoutId(EntityTypeBuilder<TEntity> builder, string tableName = null) {
            if ( tableName != null ) {
                builder.ToTable(tableName);
            }

            CreatePropertiesConfigurations(builder);
        }
        protected void CreatePropertiesConfigurations(EntityTypeBuilder<TEntity> builder) {
            builder.Property("DateCreated")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder.Property("DateUpdated")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder.Property("Deleted")
                .HasDefaultValue(false);
        }
        //create function public.getdate() returns timestamptz
        //  stable language sql as 'select now()';
    }
}
