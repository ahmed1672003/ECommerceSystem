using ECommerce.Domain.IRepositories.IIdentityRepositories;

namespace ECommerce.Domain.IRepositories;
public interface IUnitOfWork : IAsyncDisposable
{
    ICategoryRepository Categories { get; }
    IRoleClaimRepository RoleClaims { get; }
    IRoleRepository Roles { get; }
    IUserClaimRepository UserClaims { get; }
    IUserLoginRepository UserLogins { get; }
    IUserRepository Users { get; }
    IUserRoleRepository UserRoles { get; }
    IUserTokenRepository UserTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
