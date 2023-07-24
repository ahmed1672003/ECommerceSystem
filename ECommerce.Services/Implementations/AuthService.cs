using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Domain.IRepositories;
using ECommerce.Models.User.Auth;
using Microsoft.Extensions.Options;

namespace ECommerce.Services.Implementations;
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _context;
    private readonly JWT _jwt;

    public AuthService(IUnitOfWork context, IOptions<JWT> jwt)
    {
        _context = context;
        _jwt = jwt.Value;
    }

    /// <summary>
    /// Used in registration new user process
    /// </summary>
    /// <param name="user">user model</param>
    /// <returns>Task of JwtSecurityToken</returns>
    public async Task<JwtSecurityToken> CreateJwtTokenAsync(User user)
    {
        var userClaims = await _context.Users.Manager.GetClaimsAsync(user);
        var roles = await _context.Users.Manager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
            roleClaims.Add(new Claim("roles", role));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model">login model</param>
    /// <returns>Task of AuthModel</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<AuthModel> GetTokenAsync(TokenRequestModel model)
    {
        throw new NotImplementedException();
    }
}
