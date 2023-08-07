using ECommerce.Application.Features.Emails.Commands.EmailCommands;
using ECommerce.Models.Email;

namespace ECommerce.API.Controllers;
[Route("api/[controller]")]
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
}
