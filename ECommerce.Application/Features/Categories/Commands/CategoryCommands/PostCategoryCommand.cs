using ECommerce.ViewModels.ViewModels.CategoryViewModels;

namespace ECommerce.Application.Features.Categories.Commands.CategoryCommands;
public record PostCategoryCommand(PostCategoryViewModel CategoryViewModel) : IRequest<Response<CategoryViewModel>>;

