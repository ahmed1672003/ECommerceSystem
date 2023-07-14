using ECommerce.Infrastructure.Repositories.IdentityRepositories;
namespace ECommerce.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceDbContext _context;
    public UnitOfWork(
        ECommerceDbContext context,
        RoleManager<Role> roleManager,
        UserManager<User> userManager,
        SignInManager<User> singInManager)
    {
        _context = context;
        Categories = new CategoryRepository(_context);
        RoleClaims = new RoleClaimRepository(_context);
        Roles = new RoleRepository(_context, roleManager);
        Users = new UserRepository(_context, userManager, singInManager);
        UserClaims = new UserClaimRepository(_context);
        UserLogins = new UserLoginRepository(_context);
        UserRoles = new UserRoleRepository(_context);
        UserTokens = new UserTokenRepository(_context);
        UserRefreshTokens = new UserRefreshTokenRepository(_context);
    }
    public ICategoryRepository Categories { get; private set; }
    public IRoleClaimRepository RoleClaims { get; private set; }
    public IRoleRepository Roles { get; private set; }
    public IUserClaimRepository UserClaims { get; private set; }
    public IUserLoginRepository UserLogins { get; private set; }
    public IUserRepository Users { get; private set; }
    public IUserRoleRepository UserRoles { get; private set; }
    public IUserTokenRepository UserTokens { get; private set; }
    public IUserRefreshTokenRepository UserRefreshTokens { get; private set; }

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);
}
