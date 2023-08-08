using System.Runtime.InteropServices;
using WebShop.Domain.Constants;
using WebShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebShop.Persistance.Data.Contexts;

namespace WebShop.Persistance.Data.Contexts.Initialisers;

public class CatalogDbContextInitialiser
{
    private readonly ILogger<CatalogDbContextInitialiser> _logger;
    private readonly CatalogDbContext _context;

    public CatalogDbContextInitialiser(ILogger<CatalogDbContextInitialiser> logger, CatalogDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // todo initial seed
        await _context.SaveChangesAsync();
    }
}
