

namespace ECommerce.Services.Interfaces;
public interface IAuthenticationServices
{
    Task<string> GenerateJWTToken(User user);
}
