namespace ECommerce.Services.Interfaces;
public interface IAuthenticationServices
{
    Task<AuthenticationViewModel> GetJWTTokenAsync(User user);
}
