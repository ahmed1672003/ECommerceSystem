namespace ECommerce.Infrastructure.Specifications;
public class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> queryable,
        ISpecification<TEntity> specification) where TEntity : class
    {
        IQueryable<TEntity> query = queryable;

        if (specification is null)
            return query;

        if (specification.FirstCriteria is not null)
            query = query.Where(specification.FirstCriteria);

        if (specification.SecondCriteria is not null)
            query = query.Where(specification.SecondCriteria);

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludesString.Aggregate(query, (current, includeString) => current.Include(includeString));

        if (specification.OrderBy is not null)
            query = query.OrderBy(specification.OrderBy);

        if (specification.OrderByDescending is not null)
            query = query.OrderByDescending(specification.OrderByDescending);

        if (specification.GroupBy is not null)
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);

        if (specification.IsPagingEnabled)
        {
            var pageNumber = specification.PaginationRequirments.pageNumber.HasValue ?
                                specification.PaginationRequirments.pageNumber.Value <= 0 ?
                                1 : specification.PaginationRequirments.pageNumber.Value : 1;

            var pageSize = specification.PaginationRequirments.pageSize.HasValue ?
                             specification.PaginationRequirments.pageSize.Value <= 0 ?
                            10 : specification.PaginationRequirments.pageSize.Value : 10;

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        return query;
    }
}
