namespace ECommerce.Models.User.Authentication;
public class RefreshTokenRequestModel
{
    public string JWT { get; set; }
    public string RefreshJWT { get; set; }
}
