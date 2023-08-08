using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Exceptions;
using WebShop.Application.Common.Interfaces;
using WebShop.Domain.Constants;
using WebShop.Persistance.Identity;

namespace WebShop.Persistance.Data.Contexts.Initialisers
{
    public class UserDbContextInitialiser
    {
        private readonly ILogger<UserDbContextInitialiser> _logger;
        private readonly UserDbContext _context;
        private readonly IIdentityService _identityService;

        public UserDbContextInitialiser(ILogger<UserDbContextInitialiser> logger, UserDbContext context, IIdentityService identityService)
        {
            _logger = logger;
            _context = context;
            _identityService = identityService;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                //await _context.Database.MigrateAsync();
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
            try
            {
                var user = await _identityService.CreateRoleAsync(Roles.User);
            }
            catch (AlreadyExistsException exception)
            {
                _logger.LogWarning(exception, "User role is already exist");
            }

            try
            {
                var admin = await _identityService.CreateRoleAsync(Roles.Administrator);
            }
            catch (AlreadyExistsException exception)
            {
                _logger.LogWarning(exception, "Admin role is already exist");
            }




            await _context.SaveChangesAsync();
        }
    }
}
