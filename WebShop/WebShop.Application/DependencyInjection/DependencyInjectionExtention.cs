using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.DependencyInjection {
    public static class DependencyInjectionExtention {
        public static IServiceCollection AddApplication(
            this IServiceCollection services) {

            services.AddMediatR((conf) => {
                conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
