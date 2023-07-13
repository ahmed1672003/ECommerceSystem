using ECommerce.ViewModels.ViewModels.CategoryViewModels;

namespace ECommerce.API.Controllers;
[Route("api/V1/[controller]/[action]")]
[ApiController]
public class CategoryController : ECommerceController
{
    public CategoryController(IMediator mediator) : base(mediator) { }

    [HttpPost, ActionName(nameof(Post))]
    public async Task<IActionResult> Post([FromBody] PostCategoryViewModel dto) =>
      NewResult(await Mediator.Send(new PostCategoryCommand(dto)));


    [HttpPut, ActionName(nameof(Put))]
    public async Task<IActionResult> Put([FromQuery] string id, [FromBody] CategoryViewModel dto) =>

         NewResult(await Mediator.Send(new PutCategoryCommand(id, dto)));


    [HttpGet, ActionName(nameof(RetrieveById))]
    public async Task<IActionResult> RetrieveById([FromQuery] string id) =>
         NewResult(await Mediator.Send(new GetCategoryByIdQuery(id)));


    [HttpGet, ActionName(nameof(RetrieveAll))]
    public async Task<IActionResult> RetrieveAll() =>
         NewResult(await Mediator.Send(new GetAllCategoriesQuery()));


    [HttpGet, ActionName(nameof(Paginate))]
    public async Task<IActionResult> Paginate([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] CategoryEnum orderBy) =>
         NewResult(await Mediator.Send(
            new GetAllCategoriesPaginatedQuery(
                pageNumber.HasValue ? pageNumber : 1,
                pageSize.HasValue ? pageSize : 10,
                orderBy)));
}
