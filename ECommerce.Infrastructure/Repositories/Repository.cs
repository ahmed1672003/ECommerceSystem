namespace ECommerce.Infrastructure.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ECommerceDbContext _context;
    protected readonly DbSet<TEntity> _entities;

    public Repository(ECommerceDbContext context)
    {
        _context = context;
        _entities = context.Set<TEntity>();
    }

    #region Commands

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task
        CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default) =>
       await _entities.AddAsync(entity, cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task
        CreateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default) =>
        await _entities.AddRangeAsync(entities, cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task
        DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default) =>
        await Task.FromResult(_entities.Remove(entity));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task
        UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        _entities.Update(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyExpression"></param>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
        IsExist(
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default)
    {
        if (filter is null)
            return await _entities.AnyAsync(cancellationToken);
        else
            return await _entities.AnyAsync(filter, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstFilter"></param>
    /// <param name="secondFilter"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByDirection"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="paginationOn"></param>
    /// <param name="includes"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

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
    /// 
    /// </summary>
    /// <param name="mandatoryFilter"></param>
    /// <param name="optionalFilter"></param>
    /// <param name="includes"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

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
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
