using ECommerce.Models.User.Auth;

namespace ECommerce.Application.Features.Users.Commands.UserCommands;
public record RefreshTokenCommand(string RefreshToken) : IRequest<Response<AuthModel>>;