using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Models;

namespace WebShop.Application.CQRS.Identity.Users.Commands.SetRole
{
    public class SetRoleCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
