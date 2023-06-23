namespace ECommerce.Domain.Entities.IdentityEntities;
[Table("UserRoles"), PrimaryKey(nameof(UserId), nameof(RoleId))]
public class UserRole : IdentityUserRole<string>
{
    #region Properties
    public override string UserId { get => base.UserId; set => base.UserId = value; }
    public override string RoleId { get => base.RoleId; set => base.RoleId = value; }
    #endregion

    #region Behaviors
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string? ToString()
    {
        return base.ToString();
    }
    #endregion
}
