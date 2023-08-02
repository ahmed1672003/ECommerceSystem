using System.Net;

using ECommerce.Models.Category;
using ECommerce.Services.IServices;

namespace ECommerce.API.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class CategoryController : ECommerceController
{
    private readonly IUnitOfServices _services;
    public CategoryController(IMediator mediator, IUnitOfServices services) : base(mediator)
    {
        _services = services;
    }

    [HttpPost, ActionName(nameof(Post))]
    public async Task<IActionResult> Post([FromBody] PostCategoryModel model) =>
      NewResult(await Mediator.Send(new PostCategoryCommand(model)));


    [HttpPut, ActionName(nameof(Put))]
    public async Task<IActionResult> Put([FromQuery] string id, [FromBody] CategoryModel model) =>

         NewResult(await Mediator.Send(new PutCategoryCommand(id, model)));

    [HttpGet, ActionName(nameof(RetrieveById))]
    public async Task<IActionResult> RetrieveById([FromQuery] string id) =>
         NewResult(await Mediator.Send(new GetCategoryByIdQuery(id)));

    [HttpGet, ActionName(nameof(RetrieveAll))]
    public async Task<IActionResult> RetrieveAll()
    {
        #region Ip Address
        var hostName = Dns.GetHostName();

        var ips = await Dns.GetHostAddressesAsync(hostName);
        Console.WriteLine(ips[1]);
        #endregion

        var host = await Dns.GetHostEntryAsync(Dns.GetHostName());


        return NewResult(await Mediator.Send(new GetAllCategoriesQuery()));
    }

    [HttpGet, ActionName(nameof(Paginate))]
    public async Task<IActionResult> Paginate(
        [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] CategoryEnum orderBy) =>

         NewResult(await Mediator.Send(
            new GetAllCategoriesPaginatedQuery(
                pageNumber.HasValue ? pageNumber : 1,
                pageSize.HasValue ? pageSize : 10,
                orderBy)));

    [HttpGet, ActionName(nameof(AddToCookeie))]
    public async Task<IActionResult> AddToCookeie()
    {
        await _services.CookieServices.AddAsync("Age", "Ahmed");
        return Ok();
    }


    [HttpGet, ActionName(nameof(DeleteFromCookie))]
    public async Task<IActionResult> DeleteFromCookie(string key)
    {
        await _services.CookieServices.DeleteAsync(key);
        return Ok();
    }


    [HttpGet, ActionName(nameof(RetrieveFromCookie))]
    public async Task<IActionResult> RetrieveFromCookie(string key)
    {
        return Ok(await _services.CookieServices.RetrieveAsync(key));
    }
}
