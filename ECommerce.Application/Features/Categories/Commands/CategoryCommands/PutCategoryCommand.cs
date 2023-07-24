namespace ECommerce.Application.Features.Categories.Commands.CategoryCommands;
public record PutCategoryCommand(string Id, CategoryModel CategoryModel) : IRequest<Response<CategoryModel>>;
