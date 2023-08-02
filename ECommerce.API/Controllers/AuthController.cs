using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Models.User;
using ECommerce.Models.User.Auth;
using ECommerce.Models.User.Authentication;

namespace ECommerce.API.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class AuthController : ECommerceController
{
    public AuthController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] PostUserModel model)
    {
        var response = await Mediator.Send(new RegisterUserCommand(model));
        //await SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);
        return NewResult(response);
    }

    [HttpPost, ActionName(nameof(Login))]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var response = await Mediator.Send(new LoginUserCommand(model));

        //// set refresh token in cookies
        //if (!string.IsNullOrEmpty(response.Data.RefreshToken))
        //    await SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);

        return NewResult(response);
    }

    [HttpPost, ActionName(nameof(AddUserToRole))]
    public async Task<IActionResult> AddUserToRole(AddUserToRoleModel model) =>
        NewResult(await Mediator.Send(new AddUserToRoleCommand(model)));

    [HttpPut, ActionName(nameof(RefreshToken))]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestModel model)
    {
        // var refreshToken = Request.Cookies["refreshToken"];
        var response = await Mediator.Send(new RefreshTokenCommand(model));

        //// update refresh token in cookies
        //if (!string.IsNullOrEmpty(response.Data.RefreshToken))
        //   await SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);

        return NewResult(response);
    }

    [HttpPatch, ActionName(nameof(RevokeToken))]
    public async Task<IActionResult> RevokeToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = await Mediator.Send(new RevokeTokenCommand(refreshToken));

        if (response.Data.IsRevoked)
            Response.Cookies.Delete("refreshToken");

        return NewResult(response);
    }

    private Task SetRefreshTokenInCookie(string refreshToken, DateTime expires)
    {
        // save refresh token in cookie
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Expires = expires,
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        return Task.CompletedTask;
    }
}

