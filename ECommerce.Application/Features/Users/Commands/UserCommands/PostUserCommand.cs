using ECommerce.ViewModels.ViewModels.UserViewModels;

namespace ECommerce.Application.Features.Users.Commands.UserCommands;
public record PostUserCommand(PostUserViewModel model) : IRequest<Response<UserViewModel>>;

