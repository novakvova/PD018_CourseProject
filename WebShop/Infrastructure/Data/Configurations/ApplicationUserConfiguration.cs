using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Persistance.Identity;

namespace WebShop.Persistance.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            #region Properties

            builder.Property(a => a.FirstName).HasMaxLength(500).IsRequired();
            builder.Property(a => a.LastName).HasMaxLength(500).IsRequired();

            #endregion
        }
    }
}
