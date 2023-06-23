namespace ECommerce.Infrastructure.Context.Configurations.IdentityConfigurations;
public class UserTokenConfigurations : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("UserTokens");

    }
}
