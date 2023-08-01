using ECommerce.Models.User.Authentication;

namespace ECommerce.Application.Features.Users.Commands.UserCommands;
public record LoginUserCommand(LoginModel Model) : IRequest<Response<AuthenticationModel>>;
