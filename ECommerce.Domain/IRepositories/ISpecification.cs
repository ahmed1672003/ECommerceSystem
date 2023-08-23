namespace ECommerce.Domain.IRepositories;
public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>> FirstCriteria { get; }
    Expression<Func<TEntity, bool>> SecondCriteria { get; }
    Expression<Func<TEntity, object>> GroupBy { get; }
    IEnumerable<Expression<Func<TEntity, object>>> Includes { get; }
    IEnumerable<string> ThenIncludes { get; }
    Expression<Func<TEntity, object>> OrderBy { get; }
    Expression<Func<TEntity, object>> OrderByDescending { get; }

    (Func<TEntity, object> PropertyExpression, Expression<Func<TEntity, object>> ValueExpression)
        ExecuteUpdateRequirments
    { get; }

    public (int? pageNumber, int? pageSize) PaginationRequirments { get; }

    bool IsPagingEnabled { get; }
}
