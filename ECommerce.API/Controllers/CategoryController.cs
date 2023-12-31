﻿using System.Net;

using ECommerce.Domain.Enums.Category;
using ECommerce.Models.Category;
using ECommerce.Services.IServices;

namespace ECommerce.API.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
//[Authorize(Roles = $"{nameof(Roles.Basic)}")]
public class CategoryController : ECommerceController
{
    private readonly IUnitOfServices _services;
    public CategoryController(IMediator mediator, IUnitOfServices services) : base(mediator)
    {
        _services = services;
    }

    [HttpPost, ActionName(nameof(Post))]
    //[Authorize(nameof(CustomClaims.Permission), Policy = Permissions.Categories.Create, Roles = nameof(Roles.SuperAdmin))]
    public async Task<IActionResult> Post([FromBody] PostCategoryModel model) =>
      NewResult(await Mediator.Send(new PostCategoryCommand(model)));

    [HttpPut, ActionName(nameof(Put))]
    //[Authorize(Roles = $"{nameof(Roles.SuperAdmin)}")]
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
        [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] CategoryOrderBy orderBy) =>

         NewResult(await Mediator.Send(
            new CategoriesPaginationQuery(
                pageNumber.HasValue ? pageNumber : 1,
                pageSize.HasValue ? pageSize : 10,
                orderBy)));
}
