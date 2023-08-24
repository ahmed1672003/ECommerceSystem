using ECommerce.Models.ResponsModels;

namespace ECommerce.Application.Features.Categories.Commands.CategoryCommands;
public record PostCategoryCommand(PostCategoryModel CategoryModel) : IRequest<Response<CategoryModel>>;

