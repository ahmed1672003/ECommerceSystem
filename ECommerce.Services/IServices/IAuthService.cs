using System.IdentityModel.Tokens.Jwt;

using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Auth;

namespace ECommerce.Services.IServices;

public interface IAuthService
{
    Task<JwtSecurityToken> CreateJwtTokenAsync(User user);
    Task<UserRefreshToken> GenerateRefreshTokenAsync();
    Task<AuthModel> RefreshTokenAsync(string token);
    Task<bool> RevokeTokenAsync(string token);
}
