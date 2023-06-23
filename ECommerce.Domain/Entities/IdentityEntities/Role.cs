
namespace ECommerce.Domain.Entities.IdentityEntities;

public class Role : IdentityRole<string>
{
    #region Properties
    public override string Id { get => base.Id; set => base.Id = value; } = Guid.NewGuid().ToString();
    public override string? Name { get => base.Name; set => base.Name = value; }
    public override string? NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
    public override string? ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
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

    public override string ToString()
    {
        return base.ToString();
    }
    #endregion
}
