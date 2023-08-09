namespace ECommerce.Domain.Entities;
[Table("Categories"), PrimaryKey(nameof(Id)), Index(nameof(Name), IsUnique = true, Name = "IX_Categories_Name")]
public class Category : Entity<string>, IEquatable<Category>
{
    #region Properties
    [MaxLength(100), MinLength(1)]
    public string Name { get; set; }
    #endregion

    #region Constructors
    public Category() =>
    Id = Guid.NewGuid().ToString();
    #endregion

    #region Behaviors

    public override int GetHashCode() => base.GetHashCode() ^ Id.GetHashCode();

    public override bool Equals(object? obj) => this.Equals(obj as Category);

    public bool Equals(Category? other) => (other is not null && Id == other.Id) || Name == other!.Name;

    public static bool operator ==(Category left, Category right) => left is null ? right is null : left.Equals(right);

    public static bool operator !=(Category left, Category right) => left is null ? right is not null : !left.Equals(right);

    #endregion
}
