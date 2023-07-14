using ECommerce.ViewModels.ViewModels.CategoryViewModels;

using Microsoft.AspNetCore.Authorization;
namespace ECommerce.API.Controllers;
[Route("api/v1/[controller]/[action]")]
[ApiController]
[Authorize]
public class CategoryController : ECommerceController
{
    public CategoryController(IMediator mediator) : base(mediator) { }


    [HttpPost, ActionName(nameof(Post))]
    public async Task<IActionResult> Post([FromBody] PostCategoryViewModel model) =>
      NewResult(await Mediator.Send(new PostCategoryCommand(model)));


    [HttpPut, ActionName(nameof(Put))]
    public async Task<IActionResult> Put([FromQuery] string id, [FromBody] CategoryViewModel model) =>

         NewResult(await Mediator.Send(new PutCategoryCommand(id, model)));

    [HttpGet, ActionName(nameof(RetrieveById))]
    public async Task<IActionResult> RetrieveById([FromQuery] string id) =>
         NewResult(await Mediator.Send(new GetCategoryByIdQuery(id)));

    [HttpGet, ActionName(nameof(RetrieveAll))]
    public async Task<IActionResult> RetrieveAll() =>
         NewResult(await Mediator.Send(new GetAllCategoriesQuery()));

    [AllowAnonymous]
    [HttpGet, ActionName(nameof(Paginate))]
    public async Task<IActionResult> Paginate(
        [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] CategoryEnum orderBy) =>

         NewResult(await Mediator.Send(
            new GetAllCategoriesPaginatedQuery(
                pageNumber.HasValue ? pageNumber : 1,
                pageSize.HasValue ? pageSize : 10,
                orderBy)));
}
