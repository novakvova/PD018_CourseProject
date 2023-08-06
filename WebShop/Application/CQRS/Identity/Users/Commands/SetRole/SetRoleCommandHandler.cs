using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Interfaces;
using WebShop.Application.Common.Models;

namespace WebShop.Application.CQRS.Identity.Users.Commands.SetRole
{
    public class SetRoleCommandHandler : IRequestHandler<SetRoleCommand, Result>
    {
        private readonly IIdentityService _identityService;
        public SetRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<Result> Handle(SetRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.AddToRoleAsync(request.UserId, request.RoleName);
            return result;
        }
    }
}
