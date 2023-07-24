namespace ECommerce.Application.Features.Categories.Queries.CategoryQueries;
public record GetCategoryByIdQuery(string Id) : IRequest<Response<CategoryModel>>;
