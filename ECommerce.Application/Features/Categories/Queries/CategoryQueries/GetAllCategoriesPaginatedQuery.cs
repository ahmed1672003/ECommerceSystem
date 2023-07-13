using ECommerce.ViewModels.ViewModels.CategoryViewModels;

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueries;
public record GetAllCategoriesPaginatedQuery(
    int? PageNumber,
    int? PageSize,
    CategoryEnum OrderBy) : IRequest<PaginationResponse<IEnumerable<CategoryViewModel>>>;
