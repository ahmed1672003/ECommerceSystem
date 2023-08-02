using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.Services;
public class IPInfoService : IIPInfoService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public IPInfoService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Task<string> RetrieveIPAddressAsync(int index = 0)
    {
        throw new NotImplementedException();
    }
}
