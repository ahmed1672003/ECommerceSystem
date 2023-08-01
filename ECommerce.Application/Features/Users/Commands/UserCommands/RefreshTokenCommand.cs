using ECommerce.Models.User.Authentication;

namespace ECommerce.Application.Features.Users.Commands.UserCommands;
public record RefreshTokenCommand(RefreshTokenRequestModel RefreshTokenRequestModel) : IRequest<Response<AuthenticationModel>>;