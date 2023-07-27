using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Models.User;
using ECommerce.Models.User.Auth;

namespace ECommerce.API.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class AuthController : ECommerceController
{
    public AuthController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] PostUserModel model) =>
        NewResult(await Mediator.Send(new RegisterUserCommand(model)));
        
    [HttpPost, ActionName(nameof(Login))]
    public async Task<IActionResult> Login(TokenRequestModel model)
    {
        var response = await Mediator.Send(new LoginUserCommand(model));

        // set refresh token in cookies
        if (!string.IsNullOrEmpty(response.Data.RefreshToken))
            SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);

        return NewResult(response);
    }

    [HttpPost, ActionName(nameof(AddUserToRole))]
    public async Task<IActionResult> AddUserToRole(AddUserToRoleModel model) =>
        NewResult(await Mediator.Send(new AddUserToRoleCommand(model)));

    [HttpGet , ActionName(nameof(RefreshToken))]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = await Mediator.Send(new RefreshTokenCommand(refreshToken));

        // update 
        if (!string.IsNullOrEmpty(response.Data.RefreshToken))
            SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);

        return NewResult(response);
    }

    private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
    {
        // save refresh token in cookie
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Expires = expires,
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}

