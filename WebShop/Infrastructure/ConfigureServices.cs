using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Constants;
using WebShop.Infrastructure.Data.Interceptors;
using WebShop.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Persistance.Identity;
using Microsoft.AspNetCore.Identity;
using WebShop.Persistance.Common.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebShop.Persistance.Data.Contexts;
using WebShop.Persistance.Data.Contexts.Initialisers;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<CatalogDbContext>((sp, options) => {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseNpgsql(connectionString);
        });

        services.AddDbContext<UserDbContext>((sp, options) => {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseNpgsql(connectionString);
        });

        services.AddScoped<ICatalogDbContext>(provider => provider.GetRequiredService<CatalogDbContext>());

        services.AddScoped<CatalogDbContextInitialiser>();
        services.AddScoped<UserDbContextInitialiser>();


        services.AddIdentityExtensions().AddEntityFrameworkStores<UserDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        });

        string? issuer = configuration.GetValue<string>("Jwt:Issuer");
        Guard.Against.Null(issuer, message: "value 'Jwt:Issuer' not found.");

        string? audience = configuration.GetValue<string>("Jwt:Audience");
        Guard.Against.Null(audience, message: "value 'Jwt:Audience' not found.");

        string? symmetricKey = configuration.GetValue<string>("Jwt:Key");
        Guard.Against.Null(symmetricKey, message: "value 'Jwt:Key' not found.");

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = "Bearer";
            o.DefaultChallengeScheme = "Bearer";
        })
            .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricKey))
            };
        });

        

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IJwtService, JwtService>();

        return services;
    }
    public static ILoggingBuilder AddLogging(this ILoggingBuilder logging, IConfiguration configuration) {
        logging.ClearProviders();
        logging.AddConsole();
        return logging;
    }
}
