namespace ECommerce.Application.Features.Categories.Commands.CategoryCommands;
public record PutCategoryCommand(string Id, CategoryDTO CategoryDTO) : IRequest<Response<CategoryDTO>>;
