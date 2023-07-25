using System.Reflection;
using WebShop.Application.Common.Behaviours;
using WebShop.Application.Common.Interfaces;
using WebShop.Application.Common.Mappings;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) {


        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        return services;
    }
}
