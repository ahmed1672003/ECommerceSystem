using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.Services;
public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CookieService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Task<string> RetrieveAsync(string key)
    {
        var result = _contextAccessor.HttpContext!.Request.Cookies[key];
        return Task.FromResult(result)!;
    }

    public Task DeleteAsync(string key)
    {
        _contextAccessor.HttpContext!.Response.Cookies.Delete(key);
        return Task.CompletedTask;
    }

    public Task AddAsync(string key, string value)
    {
        _contextAccessor.HttpContext!.Response.Cookies.Append(key, value);
        return Task.CompletedTask;
    }
}
