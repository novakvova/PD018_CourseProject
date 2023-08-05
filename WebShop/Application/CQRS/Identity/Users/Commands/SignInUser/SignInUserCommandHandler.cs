using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Interfaces;
using WebShop.Application.Common.Models;
using WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory;

namespace WebShop.Application.CQRS.Identity.Users.Commands.SignInUser   
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, SignInUserCommandDto>
    {
        private readonly IIdentityService _identityService;
        public SignInUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<SignInUserCommandDto> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SignInAsync(request.Email, request.Password);
            SignInUserCommandDto dto = new SignInUserCommandDto()
            {
                Result = result.Result,
                Token = result.Token,
                FirstName = result.FirstName
            };
            return dto;
        }
    }
}
