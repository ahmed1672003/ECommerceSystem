namespace ECommerce.Domain.Entities.IdentityEntities;

[Table("UserRefreshTokens"), PrimaryKey(nameof(Id))]
public class UserRefreshToken
{
    public string Id { get; set; }
    public bool IsActive { get; set; } = false;
    public bool IsRevoked { get; set; } = false;
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string? JwtId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public UserRefreshToken() => Id = Guid.NewGuid().ToString();
}