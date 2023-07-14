using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Exceptions;
using WebShop.Application.Interfaces;

namespace WebShop.Persistence.DependencyInjection {
    public static class DependencyInjectionExtention {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];

            // add all DbContextes
            services.AddDbContext<CategoriesDbContext>(options => options.UseNpgsql(connectionString));

            // register all DbContextes as a service
            services.AddScoped<ICategoriesDbContext>(provider =>
                provider.GetService<CategoriesDbContext>() ?? throw new ServiceNotRegisteredException(nameof(CategoriesDbContext)));

            return services;
        }

    }
}
