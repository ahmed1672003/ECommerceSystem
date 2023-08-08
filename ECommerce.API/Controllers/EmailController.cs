using ECommerce.Application.Features.Emails.Commands.EmailCommands;
using ECommerce.Models.Email;

namespace ECommerce.API.Controllers;
[Route("api/v1/[controller]/[action]")]
[ApiController]
public class EmailController : ECommerceController
{
    public EmailController(IMediator mediator) : base(mediator) { }

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail([FromForm] SendEmailModel model)
    {
        var response = await Mediator.Send(new SendEmailCommand(model));
        return NewResult(response);
    }

    [HttpGet, ActionName(nameof(ConfirmEmail))]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequestModel model)
    {
        var response = await Mediator.Send(new ConfirmEmailCommand(model));
        return NewResult(response);
    }
}
