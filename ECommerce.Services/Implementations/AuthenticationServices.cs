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
        DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpireDate)]
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
            throw new DbUpdateException();
        }
    }

    public Task<AuthenticationViewModel>
        GetRefreshTokenAsync(string accessToken, string refreshToken)
    {
        // Decode Token and read it To Get Claims


        // Get User 
        // Validation Token , RefreshToken
        // Generate RefreshToken
        throw new NotImplementedException();
    }
    #endregion
}
