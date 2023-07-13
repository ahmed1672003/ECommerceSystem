namespace ECommerce.API.Controllers;
[Route("api/V1/[controller]/[action]")]
[ApiController]
public class UserController : ECommerceController
{
    public UserController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(Post))]
    public async Task<IActionResult> Post(PostUserViewModel model) =>
         NewResult(await Mediator.Send(new PostUserCommand(model)));


    [HttpGet, ActionName(nameof(GetUserById))]
    public async Task<IActionResult> GetUserById([FromQuery] string id) =>
         NewResult(await Mediator.Send(new GetUserByIdQuery(id)));


    [HttpGet, ActionName(nameof(GetAllUsers))]
    public async Task<IActionResult> GetAllUsers() =>
         NewResult(await Mediator.Send(new GetAllUsersQuery()));

}
