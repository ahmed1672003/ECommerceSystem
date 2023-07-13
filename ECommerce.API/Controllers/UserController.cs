namespace ECommerce.API.Controllers;
[Route("api/V1/[controller]/[action]")]
[ApiController]
public class UserController : ECommerceController
{
    public UserController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(Post))]
    public async Task<IActionResult> Post(PostUserViewModel dto)
    {
        var response = await Mediator.Send(new PostUserCommand(dto));
        return NewResult(response);
    }

    [HttpGet, ActionName(nameof(GetUserById))]
    public async Task<IActionResult> GetUserById([FromQuery] string id)
    {
        var response = await Mediator.Send(new GetUserByIdQuery(id));
        return NewResult(response);
    }

    [HttpGet, ActionName(nameof(GetAllUsers))]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await Mediator.Send(new GetAllUsersQuery());
        return NewResult(response);
    }

}
