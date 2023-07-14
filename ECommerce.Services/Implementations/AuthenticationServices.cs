namespace ECommerce.Services.Implementations;
public class AuthenticationServices : IAuthenticationServices
{
    private readonly JwtSettings _jwtSettings;
    private readonly ConcurrentDictionary<string, RefreshTokenViewModel> _userRefreshToken;
    public AuthenticationServices(
        JwtSettings jwtSettings,
        ConcurrentDictionary<string,
        RefreshTokenViewModel> userRefreshToken)
    {
        _jwtSettings = jwtSettings;
        _userRefreshToken = userRefreshToken;
    }
    private RefreshTokenViewModel this[string userName, string email]
    {
        get
        {
            var refreshToken = new RefreshTokenViewModel
            {
                ExpireAt = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpireDate),
                UserName = userName,
                Email = userName,
                NewToken = this[64],
            };
            _userRefreshToken.AddOrUpdate(
           refreshToken.NewToken, refreshToken,
           (newToken, token) => refreshToken);
            return refreshToken;
        }
    }
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
    private IEnumerable<Claim> this[User user] => new List<Claim>()
        {
            new Claim (nameof(UserClaimServiceModel.Id) , user.Id),
            new (nameof(UserClaimServiceModel.UserName) , user.UserName),
            new (nameof(UserClaimServiceModel.Email) , user.Email),
            new (nameof(UserClaimServiceModel.PhoneNumber) , user.PhoneNumber),
        };
    public Task<AuthenticationViewModel> GetJWTTokenAsync(User user)
    {
        var claims = this[user];

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            //notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpireDate),
            signingCredentials: new(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256Signature));
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult(new AuthenticationViewModel
        {
            AccessToken = accessToken,
            RefreshTokenViewModel = this[user.UserName, user.Email]
        });
    }
}
