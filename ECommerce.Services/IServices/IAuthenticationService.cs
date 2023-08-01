using System.IdentityModel.Tokens.Jwt;

using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Authentication;

namespace ECommerce.Services.IServices;
public interface IAuthenticationService
{
    Func<string, JwtSecurityToken, Task<bool>> IsJWTValid { get; }
    Task<AuthenticationModel> GetJWTAsync(User user);
    Task<JwtSecurityToken> ReadJWTAsync(string jwt);
    Task<AuthenticationModel> RefreshJWTAsync(User user);
}
