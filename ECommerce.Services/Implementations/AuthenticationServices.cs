using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using ECommerce.Services.Helpers.AuthenticationHelpers;

using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Services.Implementations;
public class AuthenticationServices : IAuthenticationServices
{
    private readonly JwtSettings _jwtSettings;

    public AuthenticationServices(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }
    public async Task<string> GenerateJWTTokenAsync(User user)
    {
        var claims = new List<Claim>()
        {
            new (nameof(UserClaimServiceModel.Id) , user.Id),
            new (nameof(UserClaimServiceModel.UserName) , user.UserName),
            new (nameof(UserClaimServiceModel.Email) , user.Email),
            new (nameof(UserClaimServiceModel.PhoneNumber) , user.PhoneNumber),
        };
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: new(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256Signature));
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return await Task.FromResult(accessToken);
    }
}
