namespace ECommerce.Services.IServices;
public interface IUnitOfServices
{
    IAuthenticationService AuthServices { get; }
    ICookieService CookieServices { get; }
    IIPInfoService IPInfoService { get; }
}
