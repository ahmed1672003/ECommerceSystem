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

    /// <summary>
    /// Delegate which pointer at result of JWT Validation (Parameraters Validation) && (Algorithm Validation)
    /// </summary>
    public Func<string, JwtSecurityToken, Task<bool>> IsJWTValid
        => async (jwt, jwtSecurityToken) =>
        ((await IsJWTParametersValidAsync(jwt)) && (await IsJWTAlgorithmValidAsync(jwtSecurityToken)));


    /// <summary>
    /// get JWT for specific user and if user does't have a jwt in data base, will get a new jwt after save in data base 
    /// </summary>
    /// <param name="user">User that you need to get JWT for him</param>
    /// <returns>Task of AuthenticationModel</returns>
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
            var accessToken = await GenerateJWTAsync(user, GetClaimsAsync);

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

    /// <summary>
    /// Get refresh token
    /// </summary>
    /// <returns>RefreshJWTModel</returns>
    private RefreshJWTModel GetRefreshToken()
    {
        var refreshToken = new RefreshJWTModel
        {
            RefreshJWTExpirationDate = DateTime.UtcNow.AddDays(_jWTSettings.RefreshTokenExpireDate),
            RefreshJWT = GenerateRefreshToken()
        };
        return refreshToken;
    }

    /// <summary>
    /// Generate refresh token
    /// </summary>
    /// <returns>refresh token string</returns>
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        var randomNumberGenerate = RandomNumberGenerator.Create();
        randomNumberGenerate.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    /// <summary>
    /// Generate JWT for specific user
    /// </summary>
    /// <param name="user">User that you need to get JWT for him</param>
    /// <param name="getClaims">Delegate that pointer get claims for specific user</param>
    /// <returns>Task of new token string</returns>
    private async Task<string> GenerateJWTAsync(User user, Func<User, Task<List<Claim>>> getClaims)
    {
        var claims = await getClaims.Invoke(user);
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

    /// <summary>
    /// Read given JWT
    /// </summary>
    /// <param name="jwt">Specific JWT for reading</param>
    /// <returns>Task of JwtSecurityToken</returns>
    public Task<JwtSecurityToken> ReadJWTAsync(string jwt)
    {
        if (string.IsNullOrEmpty(jwt) || string.IsNullOrWhiteSpace(jwt))
            return null;

        return Task.FromResult(new JwtSecurityTokenHandler().ReadJwtToken(jwt));
    }

    /// <summary>
    /// Check validation at given jwt parameters 
    /// </summary>
    /// <param name="jwt">Specific JWT</param>
    /// <returns>Task of boolean (<see langword="true"/> or <see langword="false"/>)</returns>
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

    /// <summary>
    /// Check validation at given jwt algorithm 
    /// </summary>
    /// <param name="jwtSecurityToken">Specific jwtSecurityToken</param>
    /// <returns>Task of boolean (<see langword="true"/> or <see langword="false"/>)</returns>
    async Task<bool> IsJWTAlgorithmValidAsync(JwtSecurityToken jwtSecurityToken) =>
        await Task.FromResult(jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature));

    /// <summary>
    /// [ Refresh For given user ] 
    /// [1] step one: Get user jwt
    /// [2] step two: If refreshJWT is not active will retrun <see cref="null"/>
    /// [3] step three: Revoke Old refreshJWT
    /// [4 , 5] step four: Generate new JWT & Generate new RefreshJWT 
    /// [6] step five: Create new UserJWT instance and add save it in data base & if saving in data base is fail , will return <see cref="null"/> 
    /// [7] step seven: Create AuthenticationModel instance and return it after fill his Props
    /// </summary>
    /// <param name="user">specific user need to refresh token</param>
    /// <returns>Task of AuthenticationModel</returns>
    public async Task<AuthenticationModel> RefreshJWTAsync(User user)
    {
        var userJWT = user.UserJWTs.FirstOrDefault(u => u.IsRefreshJWTActive);

        if (userJWT is null)
            return null;

        // revoke refresh JWT
        userJWT.RefreshJWTRevokedDate = DateTime.UtcNow;

        var jwt = await GenerateJWTAsync(user, GetClaimsAsync);
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
