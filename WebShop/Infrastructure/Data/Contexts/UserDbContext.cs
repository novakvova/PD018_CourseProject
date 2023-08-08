using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Infrastructure.Data;
using WebShop.Infrastructure.Data.Configurations;
using WebShop.Persistance.Data.Configurations;
using WebShop.Persistance.Identity;

namespace WebShop.Persistance.Data.Contexts
{
    public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
