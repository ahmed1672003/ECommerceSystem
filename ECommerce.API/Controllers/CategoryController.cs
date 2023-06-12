using ECommerce.Application.Features.Categories.Queries.CategoryQueries;

namespace ECommerce.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CategoryController : ECommerceController
{
    public CategoryController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(Post))]
    public async Task<IActionResult> Post([FromForm] PostCategoryDTO dto)
    {
        var response = await Mediator.Send(new PostCategoryCommand(dto));
        return NewResult(response);
    }

    [HttpPut, ActionName(nameof(Put))]
    public async Task<IActionResult> Put([FromQuery] string id, [FromForm] CategoryDTO dto)
    {
        var response = await Mediator.Send(new PutCategoryCommand(id, dto));
        return NewResult(response);
    }

    [HttpGet, ActionName(nameof(RetrieveAll))]
    public async Task<IActionResult> RetrieveAll()
    {
        var response = await Mediator.Send(new GetAllCategoriesQuery());
        return NewResult(response);
    }

    [HttpGet, ActionName(nameof(RetrieveById))]
    public async Task<IActionResult> RetrieveById(string id)
    {
        var response = await Mediator.Send(new GetCategoryByIdQuery(id));
        return NewResult(response);
    }
}
