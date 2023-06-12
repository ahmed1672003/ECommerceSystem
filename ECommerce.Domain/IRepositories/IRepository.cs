

namespace ECommerce.Domain.IRepositories;
public interface IRepository<TEntity> where TEntity : class
{
    #region Commands
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdatedRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task ExecuteUpdateAsync(
        Func<TEntity, object> property,
        Func<TEntity, object> propertyExpression,
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task ExecuteDeleteAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default);
    #endregion

    #region Queries
    Task<TEntity> RetrieveAsync(
        Expression<Func<TEntity, bool>> mandatoryFilter,
        Expression<Func<TEntity, bool>> optionalFilter = null,
        CancellationToken cancellationToken = default);
    Task<IQueryable<TEntity>> RetrieveAllAsync(
        Expression<Func<TEntity, bool>> firstFilter = null,
        Expression<Func<TEntity, bool>> secondFilter = null,
        Expression<Func<TEntity, object>> orderBy = null,
        OrderByDirection orderByDirection = OrderByDirection.Ascending,
        int? take = null,
        int? skip = null,
        CancellationToken cancellationToken = default);
    Task<bool> IsExist(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default);
    #endregion
}

