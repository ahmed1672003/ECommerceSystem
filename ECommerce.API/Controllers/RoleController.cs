using ECommerce.Application.Features.Roles.Commands.RoleCommands;
using ECommerce.Models.Role;

namespace ECommerce.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class RoleController : ECommerceController
{
    public RoleController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(Post))]
    public async Task<IActionResult> Post(PostRoleModel model)
    {
        var response = await Mediator.Send(new PostRoleCommand(model));
        return NewResult(response);
    }
}
