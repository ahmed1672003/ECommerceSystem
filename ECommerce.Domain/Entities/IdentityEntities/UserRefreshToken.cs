namespace ECommerce.Domain.Entities.IdentityEntities;
[Owned, Table("UserRefreshTokens")]
public class UserRefreshToken
{
    //public string Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
    public bool IsTokenExpired => DateTime.UtcNow > ExpiresOn;
    public DateTime CreatedOn { get; set; }
    public DateTime? RevokedOn { get; set; }
    public bool IsActive => RevokedOn == null && !IsTokenExpired;
    //public string UserId { get; set; }

    //[ForeignKey(nameof(UserId))]
    //public User User { get; set; }
    //public UserRefreshTokens() =>
    //    UserId = Guid.NewGuid().ToString();
}