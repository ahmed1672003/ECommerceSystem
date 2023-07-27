using System.Text.Json.Serialization;

namespace ECommerce.Models.User.Auth;
public class AuthModel
{
    public bool IsAuthenticated { get; set; }
    public string? Message { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public IEnumerable<string>? Roles { get; set; }
    public string? Token { get; set; }

    //public DateTime ExpiresOn { get; set; }

    [JsonIgnore]
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}
