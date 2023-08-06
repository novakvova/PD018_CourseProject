using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Mappings;
using WebShop.Application.Common.Models;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryList;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Identity.Users.Commands.SignInUser   
{
    public class SignInUserCommandDto
    {
        public Result Result { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string FirstName { get; set; } = null!;

    }
}
