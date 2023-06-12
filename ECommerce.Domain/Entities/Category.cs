namespace ECommerce.Domain.Entities;

[Table("Categories"), PrimaryKey(nameof(Id)), Index(nameof(Name), IsUnique = true)]
public class Category : Entity<string>
{
    [MaxLength(100), MinLength(1)]
    public string Name { get; set; }

    public bool IsDeleted { get; set; }
    public Category()
    {
        Id = Guid.NewGuid().ToString();
        IsDeleted = false;
    }
}
