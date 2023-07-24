namespace ECommerce.Domain.Entities.IdentityEntities;
[Owned, PrimaryKey(nameof(Id), nameof(UserId))]
public class RefreshToken
{
    public string Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
    public bool IsExpired => DateTime.UtcNow > ExpiresOn;
    public DateTime CreatedOn { get; set; }
    public DateTime? RevokedOn { get; set; }
    public bool IsActive => RevokedOn == null && !IsExpired;
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    public RefreshToken() =>
        UserId = Guid.NewGuid().ToString();
}