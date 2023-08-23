namespace ECommerce.Infrastructure.Specifications;
public class Specification<TEntity> : ISpecification<TEntity>
{
    public Specification() { }

    public Specification(Expression<Func<TEntity, bool>> firstCriteria, Expression<Func<TEntity, bool>> secondCriteria = null)
    {
        FirstCriteria = firstCriteria;
        SecondCriteria = secondCriteria;
    }

    public Expression<Func<TEntity, bool>> FirstCriteria { get; protected set; }

    public Expression<Func<TEntity, bool>> SecondCriteria { get; protected set; }

    public List<Expression<Func<TEntity, object>>> Includes { get; private set; }
        = new List<Expression<Func<TEntity, object>>>();

    public List<string> IncludesString { get; private set; } = new List<string>();

    public Expression<Func<TEntity, object>> OrderBy { get; private set; }

    public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

    public (Func<TEntity, object> PropertyExpression, Expression<Func<TEntity, object>> ValueExpression) ExecuteUpdateRequirments { get; private set; }

    public (int? pageNumber, int? pageSize) PaginationRequirments { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public Expression<Func<TEntity, object>> GroupBy { get; private set; }

    protected virtual void AddIncludes(
                          Expression<Func<TEntity, object>> include) =>
       Includes.Add(include);
    protected virtual void AddIncludesString(
                           string includesString) =>
        IncludesString.Add(includesString);

    protected virtual void AddOrderBy(
                           Expression<Func<TEntity, object>> orderBy) =>
        OrderBy = orderBy;
    protected virtual void AddOrderByDescending(
                           Expression<Func<TEntity, object>> orderByDescending) =>
        OrderByDescending = orderByDescending;
    protected virtual void AddExecuteUpdate(
                            (Func<TEntity, object> property,
                           Expression<Func<TEntity, object>> propertyExpression) executeUpdateRequirments) =>
        ExecuteUpdateRequirments = executeUpdateRequirments;
    protected virtual void ApplyPaging(
                                      (int? pageNumber, int? pageSize) paginationRequirments)
    {
        PaginationRequirments = paginationRequirments;
        IsPagingEnabled = true;
    }

    protected virtual void ApplyGroupBy(Expression<Func<TEntity, object>> groupBy) => GroupBy = groupBy;
}
