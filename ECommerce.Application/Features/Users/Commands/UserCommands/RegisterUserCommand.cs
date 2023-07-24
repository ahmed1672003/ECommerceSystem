using ECommerce.Models.User;
using ECommerce.Models.User.Auth;

namespace ECommerce.Application.Features.Users.Commands.UserCommands;
public record RegisterUserCommand(PostUserModel model) : IRequest<Response<AuthModel>>;
