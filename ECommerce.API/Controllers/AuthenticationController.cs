using ECommerce.Application.Features.Authentication.Commands.AuthenticationCommands;
using ECommerce.Application.Features.Authentication.Queries.AuthenticationQueries;
using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

namespace ECommerce.API.Controllers;
[Route("api/v1/[controller]/[action]")]
[ApiController]
public class AuthenticationController : ECommerceController
{
    public AuthenticationController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(SignIn))]
    public async Task<IActionResult> SignIn([FromBody] SignInViewModel model) =>
         NewResult(await Mediator.Send(new SignInCommand(model)));

    [HttpPut, ActionName(nameof(RefreshToken))]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand request) =>
        NewResult(await Mediator.Send(request));

    [HttpGet, ActionName(nameof(GetUserRefreshTokenByAccessToken))]
    public async Task<IActionResult> GetUserRefreshTokenByAccessToken([FromQuery] string accessToken) =>
        NewResult(await Mediator.Send(new GetUserRefreshTokenQuery(accessToken)));
}
