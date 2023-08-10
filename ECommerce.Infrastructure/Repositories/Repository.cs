using ECommerce.Domain.Enums.Shared;

namespace ECommerce.Infrastructure.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ECommerceDbContext _context;
    protected readonly DbSet<TEntity> _entities;

    public Repository(ECommerceDbContext context)
    {
        _context = context;
        _entities = context.Set<TEntity>();
    }

    #region Commands
    /// <summary>
    /// Create Given Model
    /// </summary>
    /// <param name="entity">Specififc Entity To Create<typeparamref name="TEntity"/></param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Task</returns>
    public virtual async Task
        CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default) =>
       await _entities.AddAsync(entity, cancellationToken);

    /// <summary>
    /// Create Given Models
    /// </summary>
    /// <param name="entities">Specififc Entities To Create <typeparamref name="TEntity"/></param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Task</returns>
    public virtual async Task
        CreateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default) =>
        await _entities.AddRangeAsync(entities, cancellationToken);

    /// <summary>
    /// Delete Given Model
    /// </summary>
    /// <param name="entity">Specififc Entity To Remove/param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Task</returns>
    public virtual async Task
        DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default) =>
        await Task.FromResult(_entities.Remove(entity));

    /// <summary>
    /// Delete All Models That Apply Filter
    /// </summary>
    /// <param name="filter">Filter</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Task</returns>
    public virtual async
        Task ExecuteDeleteAsync(
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default)
    {
        if (filter is null)
            await _entities.ExecuteDeleteAsync(cancellationToken);
        else
            await _entities.Where(filter).ExecuteDeleteAsync(cancellationToken);
    }

    /// <summary>
    /// Update Given Model
    /// </summary>
    /// <param name="entity">Specific Entity To Update<typeparamref name="TEntity"/></param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Task</returns>
    public virtual Task
        UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        _entities.Update(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Uodate Models
    /// </summary>
    /// <param name="entities">Specific Entities To Update</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Task</returns>
    public virtual Task
        UpdatedRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        if (entities is not null)
            _entities.UpdateRange(entities);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Update All Models That Apply Filter
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyExpression"></param>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Task</returns>
    public virtual async Task
        ExecuteUpdateAsync(
        Func<TEntity, object> property,
        Expression<Func<TEntity, object>> propertyExpression,
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default)
    {
        if (filter is null)
            await _entities
                .ExecuteUpdateAsync(entity =>
                    entity.SetProperty(property, propertyExpression), cancellationToken);
        else
            await _entities
                .Where(filter)
                .ExecuteUpdateAsync(entity =>
                    entity.SetProperty(property, propertyExpression), cancellationToken);
    }
    #endregion

    #region Queries
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<bool>
        IsExistAsync(
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default)
    {
        if (filter is null)
            return await _entities.AnyAsync(cancellationToken);
        else
            return await _entities.AnyAsync(filter, cancellationToken);
    }

    /// <summary>
    /// Retrieve All Models That Apply FirstFilter And SecondFilter, 
    /// Order Data By OrderBy And OrderByDirection, 
    /// Can Run Pagination If Needed With PageSize & PageNumber,
    /// Can Include Data By Navigation Properties By Filling Includes Array,
    /// </summary>
    /// <param name="firstFilter">FirstFilter <typeparamref name="TEntity"/></param>
    /// <param name="secondFilter">SecondFilter <typeparamref name="TEntity"/></param>
    /// <param name="orderBy">OrderBy <typeparamref name="TEntity"/></param>
    /// <param name="orderByDirection">OrderByDirection <typeparamref name="TEntity"/></param>
    /// <param name="pageNumber">PageNumber <typeparamref name="TEntity"/></param>
    /// <param name="pageSize">PageSize <typeparamref name="TEntity"/></param>
    /// <param name="paginationOn">PaginationOn <typeparamref name="TEntity"/></param>
    /// <param name="includes">Includes <typeparamref name="TEntity"/></param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Task Of IQueryable Of TEntity</returns>
    public virtual async Task<IQueryable<TEntity>>
        RetrieveAllAsync(
        Expression<Func<TEntity, bool>> firstFilter = null,
        Expression<Func<TEntity, bool>> secondFilter = null,
        Expression<Func<TEntity, object>> orderBy = null,
        OrderByDirection orderByDirection = OrderByDirection.Ascending,
        int? pageNumber = null,
        int? pageSize = null,
        bool paginationOn = false,
        string[] includes = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> entities = _entities.AsQueryable();

        if (firstFilter is not null)
            entities = entities.Where(firstFilter);

        if (secondFilter is not null)
            entities = entities.Where(secondFilter);

        if (orderBy is not null)
        {
            if (orderByDirection == OrderByDirection.Ascending)
                entities = entities.OrderBy(orderBy);
            else
                entities = entities.OrderByDescending(orderBy);
        }

        if (paginationOn)
        {
            pageNumber = pageNumber.HasValue ? pageNumber.Value <= 0 ? 1 : pageNumber.Value : 1;
            pageSize = pageSize.HasValue ? pageSize.Value <= 0 ? 10 : pageSize.Value : 10;
            entities = entities.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        if (includes is not null)
            foreach (var include in includes)
                entities = entities.Include(include);

        return await Task.FromResult(entities);
    }

    /// <summary>
    /// Retrieve Model That Apply MandatoryFilter And OPtionalFilter, 
    /// Can Include Data By Navigation Properties By Filling Includes Array,
    /// </summary>
    /// <param name="mandatoryFilter">MandatoryFilter</param>
    /// <param name="optionalFilter">OPtionalFilter</param>
    /// <param name="includes">Includes</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Task Of Entity</returns>
    public virtual async Task<TEntity>
        RetrieveAsync(
        Expression<Func<TEntity, bool>> mandatoryFilter,
        Expression<Func<TEntity, bool>> optionalFilter = null,
        string[] includes = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> entities = _entities.AsQueryable();

        if (optionalFilter is null)
            entities = entities.Where(mandatoryFilter);
        else
            entities = entities.Where(optionalFilter).Where(optionalFilter);

        if (includes is not null)
            foreach (var include in includes)
                entities = entities.Include(include);

        return await entities.FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get Count Of Entities That Apply Filter
    /// </summary>
    /// <param name="filter">Filter</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Task Of Int23</returns>
    public virtual async Task<int>
        CountAsync(
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default)
    {
        if (filter is null)
            return await _entities.CountAsync(cancellationToken);
        else
            return await _entities.CountAsync(filter, cancellationToken);
    }
    #endregion
}
