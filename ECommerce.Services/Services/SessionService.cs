using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.Services;
public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public SessionService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
}
