using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

namespace ECommerce.Application.Features.Authentication.Commands.AuthenticationCommands;
public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<Response<AuthenticationViewModel>>;