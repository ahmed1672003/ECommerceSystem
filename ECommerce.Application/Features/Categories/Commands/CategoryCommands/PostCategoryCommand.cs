namespace ECommerce.Application.Features.Categories.Commands.CategoryCommands;
public record PostCategoryCommand(PostCategoryDTO CategoryDTO) : IRequest<Response<CategoryDTO>>;

