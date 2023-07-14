namespace ECommerce.Services.Interfaces;
public interface IAuthenticationServices
{
    Task<AuthenticationViewModel> GetJWTTokenAsync(User user);
    Task<AuthenticationViewModel> GetJWTTokenAsync(string accessToken, string refreshToken);

}
