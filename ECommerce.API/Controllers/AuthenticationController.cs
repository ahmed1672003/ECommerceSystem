namespace ECommerce.API.Controllers;
[Route("api/V1/[controller]/[action]")]
[ApiController]
public class AuthenticationController : ECommerceController
{
    public AuthenticationController(IMediator mediator) : base(mediator) { }


        
}
