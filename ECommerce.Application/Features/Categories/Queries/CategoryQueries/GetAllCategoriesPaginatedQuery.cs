namespace ECommerce.Application.Features.Categories.Queries.CategoryQueries;
public record GetAllCategoriesPaginatedQuery(int? PageNumber, int? PageSize) : IRequest<PaginationResponse<IEnumerable<CategoryDTO>>>;
