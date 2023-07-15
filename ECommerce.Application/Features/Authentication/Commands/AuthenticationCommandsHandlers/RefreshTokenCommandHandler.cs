using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

namespace ECommerce.Application.Features.Authentication.Commands.AuthenticationCommandsHandlers;
public class RefreshTokenCommandHandler :
    ResponseHandler,
    IRequestHandler<RefreshTokenCommand, Response<AuthenticationViewModel>>
{
    private readonly IAuthenticationServices _authenticationServices;
    public RefreshTokenCommandHandler(IUnitOfWork context, IMapper mapper, IAuthenticationServices authenticationServices) : base(context, mapper)
    {
        _authenticationServices = authenticationServices;
    }

    public async Task<Response<AuthenticationViewModel>>
        Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var response =
            await _authenticationServices.GetRefreshTokenAsync(request.AccessToken, request.RefreshToken);

        return Success(response);
    }
}
