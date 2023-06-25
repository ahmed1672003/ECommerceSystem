

namespace ECommerce.Services.Interfaces;
public interface IAuthenticationServices
{
    Task<string> GenerateJWTTokenAsync(User user);
}
