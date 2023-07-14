namespace ECommerce.ViewModels.ViewModels.AuthenticationViewModels;
public class AuthenticationViewModel
{
    public string AccessToken { get; set; }
    public RefreshTokenViewModel RefreshTokenViewModel { get; set; }

}
public class RefreshTokenViewModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string NewToken { get; set; }
    public DateTime ExpireAt { get; set; }
}
