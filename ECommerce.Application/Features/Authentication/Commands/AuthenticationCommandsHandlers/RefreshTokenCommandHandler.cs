using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

namespace ECommerce.Application.Features.Authentication.Commands.AuthenticationCommandsHandlers;
public class RefreshTokenCommandHandler :
    ResponseHandler,
    IRequestHandler<RefreshTokenCommand, Response<AuthenticationViewModel>>
{
    public RefreshTokenCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public Task<Response<AuthenticationViewModel>>
        Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
