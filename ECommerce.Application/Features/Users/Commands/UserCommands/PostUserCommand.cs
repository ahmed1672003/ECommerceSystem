using ECommerce.ViewModels.ViewModels.UserViewModels;

namespace ECommerce.Application.Features.Users.Commands.UserCommands;
public record PostUserCommand(PostUserViewModel DTO) : IRequest<Response<UserViewModel>>;

