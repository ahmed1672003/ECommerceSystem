using ECommerce.Models.User.Auth;

namespace ECommerce.Application.Features.Users.Commands.UserCommands;
public record LoginUserCommand(TokenRequestModel Model) : IRequest<Response<AuthModel>>;
