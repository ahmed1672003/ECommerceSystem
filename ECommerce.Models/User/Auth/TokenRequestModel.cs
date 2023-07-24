namespace ECommerce.Models.User.Auth;
public class TokenRequestModel
{
    public string EmailOrUserName { get; set; }
    public string Password { get; set; }
}
