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
    public async Task<IActionResult> Login(TokenRequestModel model) =>
        NewResult(await Mediator.Send(new LoginUserCommand(model)));

    [HttpPost, ActionName(nameof(AddUserToRole))]
    public async Task<IActionResult> AddUserToRole(AddUserToRoleModel model) =>
        NewResult(await Mediator.Send(new AddUserToRoleCommand(model)));
}

