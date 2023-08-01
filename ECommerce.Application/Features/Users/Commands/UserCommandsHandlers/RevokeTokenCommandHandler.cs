using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Models.User.Auth;
using ECommerce.Services.IServices;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class RevokeTokenCommandHandler :
    ResponseHandler,
    IRequestHandler<RevokeTokenCommand, Response<RevokeTokenModel>>
{
    private readonly IAuthService _authService;
    public RevokeTokenCommandHandler(IUnitOfWork context, IMapper mapper, IAuthService authService) : base(context, mapper)
    {
        _authService = authService;
    }

    public async Task<Response<RevokeTokenModel>>
        Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.RefreshToken) || string.IsNullOrWhiteSpace(request.RefreshToken))
            return BadRequest(new RevokeTokenModel());

        var revokeTokenModel = new RevokeTokenModel()
        {
            IsRevoked = await _authService.RevokeTokenAsync(request.RefreshToken)
        };

        if (!revokeTokenModel.IsRevoked)
            return BadRequest(new RevokeTokenModel());

        return Success(revokeTokenModel);
    }
}
