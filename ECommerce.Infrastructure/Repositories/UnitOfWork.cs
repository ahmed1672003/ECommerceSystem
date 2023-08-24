using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly IECommerceDbContext _context;
    public UnitOfWork(
        IECommerceDbContext context,
        ICategoryRepository categories,
        IRoleClaimRepository roleClaims,
        IRoleRepository roles,
        IUserClaimRepository userClaims,
        IUserLoginRepository userLogins,
        IUserRepository users,
        IUserRoleRepository userRoles,
        IUserTokenRepository userTokens,
        IUserJWTRepository userJWTTokens)
    {
        _context = context;
        Categories = categories;
        RoleClaims = roleClaims;
        Roles = roles;
        UserClaims = userClaims;
        UserLogins = userLogins;
        Users = users;
        UserRoles = userRoles;
        UserTokens = userTokens;
        UserJWTs = userJWTTokens;

    }
    public ICategoryRepository Categories { get; private set; }
    public IRoleClaimRepository RoleClaims { get; private set; }
    public IRoleRepository Roles { get; private set; }
    public IUserClaimRepository UserClaims { get; private set; }
    public IUserLoginRepository UserLogins { get; private set; }
    public IUserRepository Users { get; private set; }
    public IUserRoleRepository UserRoles { get; private set; }
    public IUserTokenRepository UserTokens { get; private set; }
    public IUserJWTRepository UserJWTs { get; private set; }
    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
        await _context.Database.CommitTransactionAsync(cancellationToken);
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
        await _context.Database.BeginTransactionAsync(cancellationToken);
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) =>
        await _context.Database.RollbackTransactionAsync(cancellationToken);
}
