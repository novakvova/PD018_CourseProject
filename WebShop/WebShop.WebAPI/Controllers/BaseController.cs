using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShop.Application.Common.Exceptions;
using WebShop.WebAPI.Common.Exceptions;

namespace WebShop.WebAPI.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseController : ControllerBase {
        private IMediator mediator = null!;

        // if this.mediator is null - get and set it from context.
        protected IMediator Mediator => mediator ??=
            HttpContext.RequestServices.GetService<IMediator>() ??
                throw new ServiceNotRegisteredException(nameof(IMediator));

        //// get user id from claims (token).
        //// if User or Identity is null - set UserId to empty
        //internal Guid UserId =>
        //    !User?.Identity?.IsAuthenticated ?? false
        //    ? Guid.Empty
        //    : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
    }
}
