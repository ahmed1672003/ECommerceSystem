using System.Net;

using ECommerce.Application.Responses.ResponseServices;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ECommerceController : ControllerBase
{
    public IMediator Mediator { get; set; }
    public ECommerceController(IMediator mediator)
    {
        Mediator = mediator;
    }
    #region Results 
    public IActionResult NewResult<TData>(Response<TData> response) where TData : class
    {
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return new OkObjectResult(response);

            case HttpStatusCode.NotFound:
                return new NotFoundObjectResult(response);

            case HttpStatusCode.Unauthorized:
                return new UnauthorizedObjectResult(response);

            case HttpStatusCode.InternalServerError:
                return new ObjectResult(response)
                {
                    StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                };
            default:
                return new BadRequestObjectResult(response);
        }
    }
    #endregion
}
