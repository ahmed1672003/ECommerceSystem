using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Domain.IRepositories;
using ECommerce.Models.User.Authentication;

using Microsoft.Extensions.Options;

namespace ECommerce.Services.Implementations;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _context;
    private readonly JwtSettings _jWTSettings;

    public AuthenticationService(IUnitOfWork context, IOptions<JwtSettings> options)
    {
        _context = context;
        _jWTSettings = options.Value;
    }

    public Func<string, JwtSecurityToken, Task<bool>> IsJWTValid
        => async (jwt, jwtSecurityToken) =>
        ((await IsJWTParametersValidAsync(jwt)) && (await IsJWTAlgorithmValidAsync(jwtSecurityToken)));

    public async Task<AuthenticationModel> GetJWTAsync(User user)
    {
        var authenticationModel = new AuthenticationModel();

        if (user.UserJWTs.Any(jwt => jwt.IsRefreshJWTActive))
        {
            var activeUserJWT = user.UserJWTs.Where(jwt => jwt.IsRefreshJWTActive).FirstOrDefault();

            authenticationModel.JWTModel = new()
            {
                JWT = activeUserJWT.JWT,
                JWTExpirationDate = activeUserJWT.JWTExpirationDate,
            };

            authenticationModel.RefreshJWTModel = new()
            {
                RefreshJWT = activeUserJWT.RefreshJWT,
                RefreshJWTExpirationDate = activeUserJWT.RefreshJWTExpirtionDate
            };
        }
        else
        {
            var accessToken = await GenerateJWTAsync(user);

            var refreshJWTModel = GetRefreshToken();

            var userJWT = new UserJWT
            {
                JWT = accessToken,
                RefreshJWT = refreshJWTModel.RefreshJWT,
                IsRefreshJWTUsed = true,
                UserId = user.Id,
                JWTExpirationDate = DateTime.UtcNow.AddDays(_jWTSettings.AccessTokenExpireDate),
                RefreshJWTExpirtionDate = DateTime.UtcNow.AddDays(_jWTSettings.RefreshTokenExpireDate)
            };

            user.UserJWTs.Add(userJWT);

            var identityResult = await _context.Users.Manager.UpdateAsync(user);

            if (!identityResult.Succeeded)
                return new AuthenticationModel();

            authenticationModel.JWTModel = new()
            {
                JWT = userJWT.JWT,
                JWTExpirationDate = userJWT.JWTExpirationDate
            };

            authenticationModel.RefreshJWTModel = new()
            {
                RefreshJWT = userJWT.RefreshJWT,
                RefreshJWTExpirationDate = userJWT.RefreshJWTExpirtionDate
            };
        }

        return authenticationModel;
    }

    private RefreshJWTModel GetRefreshToken()
    {
        var refreshToken = new RefreshJWTModel
        {
            RefreshJWTExpirationDate = DateTime.UtcNow.AddDays(_jWTSettings.RefreshTokenExpireDate),
            RefreshJWT = GenerateRefreshToken()
        };
        return refreshToken;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        var randomNumberGenerate = RandomNumberGenerator.Create();
        randomNumberGenerate.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task<string> GenerateJWTAsync(User user)
    {
        var claims = await GetClaimsAsync(user);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jWTSettings.Secret));
        var jwtToken =
              new JwtSecurityToken(
              issuer: _jWTSettings.Issuer,
              audience: _jWTSettings.Audience,
              claims: claims,
              expires: DateTime.UtcNow.AddDays(_jWTSettings.AccessTokenExpireDate),
              signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature));
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return accessToken;
    }

    private async Task<List<Claim>> GetClaimsAsync(User user)
    {
        var roles = await _context.Users.Manager.GetRolesAsync(user);
        var userClaims = await _context.Users.Manager.GetClaimsAsync(user);

        var claims = new List<Claim>()
        {
            new (ClaimTypes.PrimarySid, user.Id),
            new (ClaimTypes.Name,user.UserName),
            new (ClaimTypes.Email,user.Email),
            new (ClaimTypes.MobilePhone, user.PhoneNumber),
        };

        foreach (var role in roles)
            claims.Add(new(ClaimTypes.Role, role));

        claims.AddRange(userClaims);

        return claims;
    }

    public Task<JwtSecurityToken> ReadJWTAsync(string jwt)
    {
        if (string.IsNullOrEmpty(jwt) || string.IsNullOrWhiteSpace(jwt))
            return null;

        return Task.FromResult(new JwtSecurityTokenHandler().ReadJwtToken(jwt));
    }

    Task<bool> IsJWTParametersValidAsync(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = _jWTSettings.ValidateIssuer,
            ValidIssuers = new[] { _jWTSettings.Issuer },
            ValidateIssuerSigningKey = _jWTSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jWTSettings.Secret)),
            ValidAudience = _jWTSettings.Audience,
            ValidateAudience = _jWTSettings.ValidateAudience,
            ValidateLifetime = _jWTSettings.ValidateLifeTime,
        };
        try
        {
            var validator = handler.ValidateToken(jwt, parameters, out SecurityToken validatedToken);

            if (validatedToken == null)
                return Task.FromResult(false);
        }
        catch
        {
            return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }

    async Task<bool> IsJWTAlgorithmValidAsync(JwtSecurityToken jwtSecurityToken) =>
        await Task.FromResult(jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature));

    public async Task<AuthenticationModel> RefreshJWTAsync(User user)
    {
        var userJWT = user.UserJWTs.FirstOrDefault(u => u.IsRefreshJWTActive);

        if (!userJWT.IsRefreshJWTActive)
            return null;

        // revoke refresh JWT
        userJWT.RefreshJWTRevokedDate = DateTime.UtcNow;

        var jwt = await GenerateJWTAsync(user);
        var refreshJWT = GenerateRefreshToken();

        // add new refresh token to user
        var newUserJWT = new UserJWT()
        {
            UserId = user.Id,
            JWT = jwt,
            JWTExpirationDate = DateTime.UtcNow.AddDays(_jWTSettings.AccessTokenExpireDate),
            RefreshJWT = refreshJWT,
            RefreshJWTExpirtionDate = DateTime.UtcNow.AddDays(_jWTSettings.RefreshTokenExpireDate),
            IsRefreshJWTUsed = true,
        };
        user.UserJWTs.Add(newUserJWT);

        var identityResult = await _context.Users.Manager.UpdateAsync(user);

        if (!identityResult.Succeeded)
            return null;

        return new AuthenticationModel()
        {
            JWTModel = new()
            {
                JWT = newUserJWT.JWT,
                JWTExpirationDate = newUserJWT.JWTExpirationDate
            },
            RefreshJWTModel = new()
            {
                RefreshJWT = newUserJWT.RefreshJWT,
                RefreshJWTExpirationDate = newUserJWT.RefreshJWTExpirtionDate
            }
        };
    }
}
