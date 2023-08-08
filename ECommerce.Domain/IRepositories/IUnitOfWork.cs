using ECommerce.Domain.IRepositories.IIdentityRepositories;

using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Domain.IRepositories;
public interface IUnitOfWork : IAsyncDisposable
{
    IDbContextTransaction Transaction { get; }
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
}
