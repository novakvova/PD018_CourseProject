using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Models;

namespace WebShop.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(int userId);

        Task<bool> IsInRoleAsync(int userId, string role);

        Task<bool> AuthorizeAsync(int userId, string policyName);

        Task<(Result Result, string Token, string FirstName)> SignInAsync(string email, string password);

        Task<(Result Result, int UserId)> CreateUserAsync(
            string password, string email, string firstName, string lastName);

        Task<Result> DeleteUserAsync(int userId);

        Task<Result> CreateRoleAsync(string roleName);

        Task<Result> AddToRoleAsync(int userId, string roleName);

    }
}
