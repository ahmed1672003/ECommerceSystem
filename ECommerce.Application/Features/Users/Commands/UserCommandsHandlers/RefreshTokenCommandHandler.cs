using System;
using System.Collections.Generic;

using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Models.User.Auth;
using ECommerce.Services.Interfaces;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class RefreshTokenCommandHandler :
    ResponseHandler,
    IRequestHandler<RefreshTokenCommand, Response<AuthModel>>
{
    private readonly IAuthService _authService;
    public RefreshTokenCommandHandler(IUnitOfWork context, IMapper mapper, IAuthService authService) : base(context, mapper)
    {
        _authService = authService;
    }

    public async Task<Response<AuthModel>> 
        Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.RefreshToken) || string.IsNullOrWhiteSpace(request.RefreshToken))
             return BadRequest<AuthModel>();

        var authModel = await _authService.RefreshTokenAsync(request.RefreshToken);

        if (!authModel.IsAuthenticated)
            return BadRequest(authModel);

        return Success(authModel);
    }
}
