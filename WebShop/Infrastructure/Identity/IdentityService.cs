using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Exceptions;
using WebShop.Application.Common.Interfaces;
using WebShop.Application.Common.Models;
using WebShop.Domain.Constants;

namespace WebShop.Persistance.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        public async Task<string?> GetUserNameAsync(int userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            return user.UserName;
        }

        public async Task<(Result Result, int UserId)> CreateUserAsync(
            string password, string email, string firstName, string lastName)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                throw new AlreadyExistsException(email, "User is already exist");
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
            };

            var result = await _userManager.CreateAsync(user, password);
            await AddToRoleAsync(user, Roles.User);
            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(int userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(int userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }
            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
            var result = await _authorizationService.AuthorizeAsync(principal, policyName);
            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(int userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.ToApplicationResult();
        }

        public async Task<(Result Result, string? Token, string? FirstName)> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException(email, nameof(ApplicationUser));
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            
            if (!isPasswordValid)
            {
                return ((await _userManager.AccessFailedAsync(user)).ToApplicationResult(), null, null);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return (
                    Result.Success(),
                    _jwtService.GenerateToken(user.Id, email, userRoles.ToArray()),
                    user.FirstName);
        }

        public async Task<Result> CreateRoleAsync(string roleName)
        {
            if ((await _roleManager.FindByNameAsync(roleName) != null))
            {
                throw new AlreadyExistsException(roleName, nameof(ApplicationRole));
            }

            var result = await _roleManager.CreateAsync
                (
                    new ApplicationRole()
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    }
                );
            return result.ToApplicationResult();
        }

        public async Task<Result> AddToRoleAsync(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));

            return await AddToRoleAsync(user, roleName);
        }

        private async Task<Result> AddToRoleAsync(ApplicationUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.ToApplicationResult();

        }
    }
}
