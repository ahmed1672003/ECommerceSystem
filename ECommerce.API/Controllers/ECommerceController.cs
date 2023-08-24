using System.Net;

using ECommerce.Models.ResponsModels;

namespace ECommerce.API.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class ECommerceController : ControllerBase
{
    public IMediator Mediator { get; set; }
    public ECommerceController(IMediator mediator) => Mediator = mediator;

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

            case HttpStatusCode.BadRequest:
                return new BadRequestObjectResult(response);

            case HttpStatusCode.Conflict:
                return new ConflictObjectResult(response);

            default:
                return new ObjectResult(response)
                {
                    StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                };
        }
    }
    #endregion
}
