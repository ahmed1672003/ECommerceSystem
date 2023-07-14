using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

namespace ECommerce.Application.Features.Authentication.Commands.AuthenticationCommands;
public record SignInCommand(SignInViewModel model) : IRequest<Response<AuthenticationViewModel>>;
