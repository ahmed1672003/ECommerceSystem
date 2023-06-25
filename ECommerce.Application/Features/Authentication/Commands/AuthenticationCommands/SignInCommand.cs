namespace ECommerce.Application.Features.Authentication.Commands.AuthenticationCommands;
public record SignInCommand(SignInDTO DTO) : IRequest<Response<string>>;
