

namespace ECommerce.Infrastructure.Context.Configurations;
public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(c => c.Name)
            .IsRequired(true)
            .HasMaxLength(100);

        builder
            .Property(c => c.IsDeleted)
            .IsRequired(true)
            .HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
