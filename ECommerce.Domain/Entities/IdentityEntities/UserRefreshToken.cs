namespace ECommerce.Domain.Entities.IdentityEntities;

[Table("UserRefreshTokens"), PrimaryKey(nameof(Id))]
public class UserRefreshToken
{
    public string Id { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpireAt { get; set; }
    public DateTime RefreshTokenExpireAt { get; set; }
    public bool IsAccessTokenActive
    {
        get => DateTime.UtcNow < AccessTokenExpireAt;
    }
    public bool IsRefreshTokenActive
    {
        get => DateTime.UtcNow < RefreshTokenExpireAt;
    }
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    public UserRefreshToken() => Id = Guid.NewGuid().ToString();
}