using ECommerce.Domain.IRepositories;

using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Implementations;
public class AuthenticationServices : IAuthenticationServices
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUnitOfWork _context;
    public AuthenticationServices(
        JwtSettings jwtSettings,
        IUnitOfWork context)
    {
        _jwtSettings = jwtSettings;
        _context = context;
    }

    #region Helper Indexes
    private RefreshTokenViewModel this[string userName, string email, DateTime refreshTokenExpireData]
    {
        get
        {
            var refreshToken = new RefreshTokenViewModel
            {
                ExpireAt = refreshTokenExpireData,
                UserName = userName,
                Email = userName,
                NewToken = this[64],
            };
            return refreshToken;
        }
    }
    private AuthenticationViewModel this[User user, string accessToken] =>
        new()
        {
            AccessToken = accessToken,
            RefreshTokenViewModel = this[
                user.UserName,
                user.Email,
        DateTime.UtcNow.AddMonths(_jwtSettings.RefreshTokenExpireDate)]
        };
    private IEnumerable<Claim> this[User user] => new List<Claim>()
        {
            new Claim (nameof(UserClaimViewModel.UserId) , user.Id),
            new (nameof(UserClaimViewModel.UserName) , user.UserName),
            new (nameof(UserClaimViewModel.Email) , user.Email),
            new (nameof(UserClaimViewModel.PhoneNumber) , user.PhoneNumber),
        };
    private string this[int length]
    {
        get
        {
            var randomNumbers = new byte[length];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumbers);
            return Convert.ToBase64String(randomNumbers);
        }
    }
    private (JwtSecurityToken Token, string AccessToken) this[User user, DateTime accessTokenExpireDate]
    {
        get
        {
            var token = new JwtSecurityToken(
             issuer: _jwtSettings.Issuer, audience: _jwtSettings.Audience,
             claims: this[user],
             //notBefore: DateTime.UtcNow,
             expires: accessTokenExpireDate,
             signingCredentials: new(
                 new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                 SecurityAlgorithms.HmacSha256Signature));
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (token, tokenString);
        }
    }
    #endregion

    #region Functions

    public async Task<AuthenticationViewModel>
        GetJWTTokenAsync(User user)
    {
        var tokenTuple =
            this[user, DateTime.UtcNow.AddMonths(_jwtSettings.AccessTokenExpireDate)];

        var authenticationViewModel = this[user, tokenTuple.AccessToken];

        var userRefreshToken = new UserRefreshToken
        {
            UserId = user.Id,
            AccessToken = tokenTuple.AccessToken,
            RefreshToken = authenticationViewModel.RefreshTokenViewModel.NewToken,
            RefreshTokenExpireAt = authenticationViewModel.RefreshTokenViewModel.ExpireAt,
            AccessTokenExpireAt = tokenTuple.Token.ValidTo,
        };

        await _context.UserRefreshTokens.CreateAsync(userRefreshToken);

        try
        {
            await _context.SaveChangesAsync();
            return await Task.FromResult(authenticationViewModel);
        }
        catch (Exception)
        {
            throw new DbUpdateException("Save changes exception !");
        }
    }

    public async Task<AuthenticationViewModel>
        GetRefreshTokenAsync(string accessToken, string refreshToken)
    {
        // Decode Token and read it To Get Claims
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = await ReadJwtTokenAsync(accessToken);

        // Check Validate Algorithm
        if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            throw new SecurityTokenException("Algorithm not valid !");

        var userIdFromJwtToken = jwtToken.Claims.FirstOrDefault(t => t.Type == nameof(UserClaimViewModel.UserId)).Value;

        // Get UserRefreshToken 
        if (!await _context.UserRefreshTokens.IsExist(e =>
        e.UserId.Equals(userIdFromJwtToken) &&
        e.AccessToken.Equals(accessToken) &&
        e.RefreshToken.Equals(refreshToken)))
            throw new ArgumentNullException("User refresh token not found !");

        var userRefreshToken = await _context.UserRefreshTokens
            .RetrieveAsync(ut => ut.UserId.Equals(userIdFromJwtToken));

        // Validation Token , RefreshToken
        if (!await ValidateTokenAsync(accessToken))
            throw new SecurityTokenException("Token is not valid !");

        if (userRefreshToken.IsAccessTokenActive)
            throw new SecurityTokenException("Token is not expired !");

        if (!userRefreshToken.IsRefreshTokenActive)
            throw new SecurityTokenException("Refresh is expired !");

        // Get User To Get New Token
        var user = await _context.Users.RetrieveAsync(u => u.Id.Equals(userIdFromJwtToken));

        // Generate RefreshToken
        var authenticationViewModel = await GetJWTTokenAsync(user);
        return authenticationViewModel;
    }

    public async Task<bool> ValidateTokenAsync(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();

        var parameters = new TokenValidationParameters()
        {
            ValidateIssuer = _jwtSettings.ValidateIssuer,
            ValidIssuers = new[] { _jwtSettings.Issuer },
            ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
            ValidAudience = _jwtSettings.Audience,
            ValidateAudience = _jwtSettings.ValidateAudience,
            ValidateLifetime = _jwtSettings.ValidateLifeTime
        };

        var validator = await handler.ValidateTokenAsync(accessToken, parameters);

        return validator.IsValid;
    }

    public Task<JwtSecurityToken> ReadJwtTokenAsync(string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
            throw new ArgumentNullException(nameof(accessToken));

        var handler = new JwtSecurityTokenHandler();

        var response = handler.ReadJwtToken(accessToken);

        return Task.FromResult(response);
    }
    #endregion
}
