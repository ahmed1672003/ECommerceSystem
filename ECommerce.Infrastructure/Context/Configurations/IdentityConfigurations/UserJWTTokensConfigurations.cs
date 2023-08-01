namespace ECommerce.Infrastructure.Context.Configurations.IdentityConfigurations;
public class UserJWTTokensConfigurations : IEntityTypeConfiguration<UserJWT>
{
    public void Configure(EntityTypeBuilder<UserJWT> builder)
    {
        builder.ToTable("UserJWTs");
    }
}
