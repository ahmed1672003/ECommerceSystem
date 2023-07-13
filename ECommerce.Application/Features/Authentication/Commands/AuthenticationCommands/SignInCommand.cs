using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

namespace ECommerce.Application.Features.Authentication.Commands.AuthenticationCommands;
public record SignInCommand(SignInViewModel DTO) : IRequest<Response<string>>;
