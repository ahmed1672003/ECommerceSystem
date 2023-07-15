namespace ECommerce.Services.Interfaces;
public interface IAuthenticationServices
{
    Task<AuthenticationViewModel> GetJWTTokenAsync(User user);
    Task<AuthenticationViewModel> GetRefreshTokenAsync(string accessToken, string refreshToken);
    Task<JwtSecurityToken> ReadJwtTokenAsync(string accessToken);
    Task<bool> ValidateTokenAsync(string accessToken);
}
