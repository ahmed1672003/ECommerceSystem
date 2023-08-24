using ECommerce.Models.ResponsModels;

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueries;
public record CategoriesPaginationQuery(
    int? PageNumber,
    int? PageSize,
    CategoryOrderBy OrderBy) : IRequest<PaginationResponse<IEnumerable<CategoryModel>>>;
