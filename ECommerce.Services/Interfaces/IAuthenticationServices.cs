namespace ECommerce.Services.Interfaces;
public interface IAuthenticationServices
{
    Task<string> GetJWTTokenAsync(User user);
}
