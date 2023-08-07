namespace ECommerce.Services.IServices;
public interface IUnitOfServices
{
    IAuthenticationService AuthServices { get; }
    ICookieService CookieServices { get; }
    IIPInfoService IPInfoService { get; }
    ISessionService SessionServices { get; }
    IEmailService EmailServices { get; }
}
