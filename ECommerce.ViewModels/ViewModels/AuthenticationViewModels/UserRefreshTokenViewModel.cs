namespace ECommerce.ViewModels.ViewModels.AuthenticationViewModels;
public class UserRefreshTokenViewModel
{
    public string TokenId { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpireAt { get; set; }
    public DateTime RefreshTokenExpireAt { get; set; }
    public bool IsAccessTokenActive { get; set; }
    public bool IsRefreshTokenActive { get; set; }
    public string UserId { get; set; }
}
