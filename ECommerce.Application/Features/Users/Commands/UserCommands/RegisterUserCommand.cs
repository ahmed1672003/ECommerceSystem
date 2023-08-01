using ECommerce.Models.User;
using ECommerce.Models.User.Authentication;

namespace ECommerce.Application.Features.Users.Commands.UserCommands;
public record RegisterUserCommand(PostUserModel Model) : IRequest<Response<AuthenticationModel>>;
