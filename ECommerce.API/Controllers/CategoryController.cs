using ECommerce.Application.Features.Categories.Queries.CategoryQueries;

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
    public async Task<IActionResult> Put([FromHeader] string id, [FromBody] CategoryDTO dto)
    {
        var response = await Mediator.Send(new PutCategoryCommand(id, dto));
        return NewResult(response);
    }

    [HttpGet("{id}"), ActionName(nameof(RetrieveById))]
    public async Task<IActionResult> RetrieveById([FromQuery] string id)
    {
        var response = await Mediator.Send(new GetCategoryByIdQuery(id));
        return NewResult(response);
    }

    [HttpGet, ActionName(nameof(RetrieveAll))]
    public async Task<IActionResult> RetrieveAll()
    {
        var response = await Mediator.Send(new GetAllCategoriesQuery());
        return NewResult(response);
    }

    [HttpGet, ActionName(nameof(RetrieveAllPaginated))]
    public async Task<IActionResult> RetrieveAllPaginated([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var response = await Mediator.Send(
            new GetAllCategoriesPaginatedQuery(
                pageNumber.HasValue ? pageNumber : 1,
                pageSize.HasValue ? pageSize : 10));
        return NewResult(response);
    }
}
