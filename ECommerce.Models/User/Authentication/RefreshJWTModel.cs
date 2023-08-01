namespace ECommerce.Models.User.Authentication;

public class RefreshJWTModel
{
    public string RefreshJWT { get; set; }
    public DateTime RefreshJWTExpirationDate { get; set; }
}