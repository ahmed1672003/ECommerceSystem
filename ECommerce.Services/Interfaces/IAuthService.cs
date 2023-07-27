using System.IdentityModel.Tokens.Jwt;

using ECommerce.Domain.Entities.IdentityEntities;

namespace ECommerce.Services.Interfaces
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> CreateJwtTokenAsync(User user);
        Task<UserRefreshToken> GenerateRefreshTokenAsync();
    }
}
