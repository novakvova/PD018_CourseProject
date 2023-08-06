using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.CQRS.Identity.Users.Commands.SignInUser   
{
    public class SignInUserCommand : IRequest<SignInUserCommandDto>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
