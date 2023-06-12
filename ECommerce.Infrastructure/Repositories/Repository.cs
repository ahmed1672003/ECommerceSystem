﻿namespace ECommerce.Infrastructure.Repositories;
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
    public virtual async Task CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default) =>
       await _entities.AddAsync(entity, cancellationToken);

    public virtual async Task CreateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default) =>
        await _entities.AddRangeAsync(entities, cancellationToken);

    public virtual async Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default) =>
        await Task.FromResult(_entities.Remove(entity));

    public virtual async Task ExecuteDeleteAsync(
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default)
    {
        if (filter is null)
            await _entities.ExecuteDeleteAsync(cancellationToken);
        else
            await _entities.Where(filter).ExecuteDeleteAsync(cancellationToken);
    }

    public virtual Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        _entities.Remove(entity);
        return Task.CompletedTask;
    }
    public virtual Task UpdatedRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        if (entities is not null)
            _entities.UpdateRange(entities);
        return Task.CompletedTask;
    }
    public virtual async Task ExecuteUpdateAsync(
        Func<TEntity, object> property,
        Func<TEntity, object> propertyExpression,
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
    public virtual async Task<bool> IsExist(
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default)
    {
        if (filter is null)
            return await _entities.AnyAsync(cancellationToken);
        else
            return await _entities.AnyAsync(filter, cancellationToken);
    }
    public virtual async Task<IQueryable<TEntity>> RetrieveAllAsync(
        Expression<Func<TEntity, bool>> firstFilter = null,
        Expression<Func<TEntity, bool>> secondFilter = null,
        Expression<Func<TEntity, object>> orderBy = null,
        OrderByDirection orderByDirection = OrderByDirection.Ascending,
        int? take = null,
        int? skip = null,
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

        return await Task.FromResult(entities);
    }

    public virtual async Task<TEntity> RetrieveAsync(
        Expression<Func<TEntity, bool>> mandatoryFilter,
        Expression<Func<TEntity, bool>> optionalFilter = null,
        CancellationToken cancellationToken = default)
    {
        if (optionalFilter is null)
            return await _entities.FirstOrDefaultAsync(mandatoryFilter, cancellationToken);
        else
            return await _entities.Where(optionalFilter).FirstOrDefaultAsync(mandatoryFilter, cancellationToken);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        if (filter is null)
            return await _entities.CountAsync(cancellationToken);
        else
            return await _entities.CountAsync(filter, cancellationToken);

    }
    #endregion
}
