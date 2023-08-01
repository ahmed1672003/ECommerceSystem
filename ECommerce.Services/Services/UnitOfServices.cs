namespace ECommerce.Services.Services;
public class UnitOfServices : IUnitOfServices
{
    public IAuthenticationService AuthServices { get; private set; }
    public ICookieService CookieServices { get; private set; }
    public IIPInfoService IPInfoService { get; private set; }

    public UnitOfServices(IAuthenticationService authServices, IIPInfoService iPInfoService, ICookieService cookieServices)
    {
        AuthServices = authServices;
        IPInfoService = iPInfoService;
        CookieServices = cookieServices;
    }
}
