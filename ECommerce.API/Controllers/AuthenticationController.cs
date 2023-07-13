using ECommerce.Application.Features.Authentication.Commands.AuthenticationCommands;
using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

namespace ECommerce.API.Controllers;
[Route("api/V1/[controller]/[action]")]
[ApiController]
public class AuthenticationController : ECommerceController
{
    public AuthenticationController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(SignIn))]
    public async Task<IActionResult> SignIn([FromBody] SignInViewModel dto)
    {
        var response = await Mediator.Send(new SignInCommand(dto));
        return NewResult(response);
    }
}
