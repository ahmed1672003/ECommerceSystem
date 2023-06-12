namespace ECommerce.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CategoryController : ECommerceController
{
    public CategoryController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(Post))]
    public async Task<IActionResult> Post([FromBody] PostCategoryDTO dto)
    {
        var response = await Mediator.Send(new PostCategoryCommand(dto));
        return NewResult(response);
    }

    [HttpPut, ActionName(nameof(Put))]
    public async Task<IActionResult> Put([FromQuery] string id, [FromBody] CategoryDTO dto)
    {
        var response = await Mediator.Send(new PutCategoryCommand(id, dto));
        return NewResult(response);
    }
}
