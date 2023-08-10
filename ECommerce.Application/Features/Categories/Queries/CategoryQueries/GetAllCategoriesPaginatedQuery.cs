

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueries;
public record GetAllCategoriesPaginatedQuery(
    int? PageNumber,
    int? PageSize,
    CategoryOrderBy OrderBy) : IRequest<PaginationResponse<IEnumerable<CategoryModel>>>;
