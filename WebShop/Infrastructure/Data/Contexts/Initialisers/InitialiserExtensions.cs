using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebShop.Persistance.Data.Contexts.Initialisers;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<UserDbContextInitialiser>();

        //await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}
