using ECommerce.ViewModels.ViewModels.CategoryViewModels;

namespace ECommerce.Application.Features.Categories.Commands.CategoryCommands;
public record PutCategoryCommand(string Id, CategoryViewModel CategoryViewModel) : IRequest<Response<CategoryViewModel>>;
