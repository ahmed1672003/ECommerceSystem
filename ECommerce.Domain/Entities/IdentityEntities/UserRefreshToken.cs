namespace ECommerce.Domain.Entities.IdentityEntities;
[Owned, Table("UserRefreshTokens")]
public class UserRefreshToken
{
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
    public bool IsTokenExpired => DateTime.UtcNow > ExpiresOn;
    public DateTime CreatedOn { get; set; }
    public DateTime? RevokedOn { get; set; }
    public bool IsActive => RevokedOn == null && !IsTokenExpired;
}