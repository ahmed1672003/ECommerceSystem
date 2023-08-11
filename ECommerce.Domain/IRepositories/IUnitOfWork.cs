using ECommerce.Domain.IRepositories.IIdentityRepositories;

using Microsoft.EntityFrameworkCore.Storage;

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
    IUserJWTRepository UserJWTs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollBackAsync(CancellationToken cancellationToken = default);
}
