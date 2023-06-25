using ECommerce.Application.Features.Authentication.AuthenticationDTOs;
using ECommerce.Application.Features.Authentication.Commands.AuthenticationCommands;

namespace ECommerce.API.Controllers;
[Route("api/V1/[controller]/[action]")]
[ApiController]
public class AuthenticationController : ECommerceController
{
    public AuthenticationController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(SignIn))]
    public async Task<IActionResult> SignIn([FromBody] SignInDTO dto)
    {
        var response = await Mediator.Send(new SignInCommand(dto));
        return NewResult(response);
    }
}
