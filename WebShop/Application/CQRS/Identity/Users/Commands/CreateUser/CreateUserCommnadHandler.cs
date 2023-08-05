using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Interfaces;
using WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory;

namespace WebShop.Application.CQRS.Identity.Users.Commands.CreateUser
{
    public class CreateUserCommnadHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IIdentityService _identityService;
        public CreateUserCommnadHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
             var result = await _identityService.CreateUserAsync(
                 request.Password, request.Email, request.LastName, request.FirstName);


            return result.UserId;
        }
    }
}
