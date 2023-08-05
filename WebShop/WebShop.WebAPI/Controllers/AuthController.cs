using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.CQRS.Identity.Users.Commands.CreateUser;
using WebShop.Application.CQRS.Identity.Users.Commands.SetRole;
using WebShop.Application.CQRS.Identity.Users.Commands.SignInUser;
using WebShop.Domain.Constants;
using WebShop.WebAPI.DTO.Auth.User;

namespace WebShop.WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IMapper mapper;
        public AuthController(IMapper mapper)
        {   
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> SignUp([FromForm] SignUpDto dto)
        {
            // map received from request dto to cqrs command
            var command = mapper.Map<CreateUserCommand>(dto);
            var userId = await Mediator.Send(command);

            return Ok(userId);
        }

        [HttpPost]
        public async Task<ActionResult> SignIn([FromForm] SignInUserDto dto)
        {
            // map received from request dto to cqrs command
            var command = mapper.Map<SignInUserCommand>(dto);
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<ActionResult> SetRole([FromForm] SetRoleDto dto)
        {
            var command = mapper.Map<SetRoleCommand>(dto);
            var result = await Mediator.Send(command);

            return Ok(result);
        } 

    }
}
