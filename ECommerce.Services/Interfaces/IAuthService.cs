using System.IdentityModel.Tokens.Jwt;

using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Auth;

namespace ECommerce.Services.Interfaces
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> CreateJwtTokenAsync(User user);


        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
    }
}
