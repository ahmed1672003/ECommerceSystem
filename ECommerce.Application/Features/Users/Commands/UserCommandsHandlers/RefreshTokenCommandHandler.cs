using System.ComponentModel.DataAnnotations;

using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Application.ResponseServices;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.ResponsModels;
using ECommerce.Models.User.Authentication;
using ECommerce.Services.IServices;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class RefreshTokenCommandHandler :
    IRequestHandler<RefreshTokenCommand, Response<AuthenticationModel>>
{
    private readonly EmailAddressAttribute _emailAddressAttribute;
    private readonly IUnitOfWork _context;
    private readonly IUnitOfServices _services;
    public RefreshTokenCommandHandler(
        IUnitOfWork context,
        IUnitOfServices services,
        IHttpContextAccessor accessor,
        EmailAddressAttribute emailAddressAttribute)
    {
        _emailAddressAttribute = emailAddressAttribute;
        _context = context;
        _services = services;
    }

    #region Refresh Token Command Handler
    public async Task<Response<AuthenticationModel>>
        Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.UserJWTs.IsExistAsync(uj =>
       uj.JWT.Equals(request.RefreshTokenRequestModel.JWT) &&
       uj.RefreshJWT.Equals(request.RefreshTokenRequestModel.RefreshJWT) &&
       uj.IsRefreshJWTUsed))
            return ResponseHandler.NotFound<AuthenticationModel>();

        var jwtSecurityToken =
            await _services.AuthServices.ReadJWTAsync(request.RefreshTokenRequestModel.JWT);

        var isJWTValid = await _services.AuthServices
            .IsJWTValid.Invoke(request.RefreshTokenRequestModel.JWT, jwtSecurityToken);

        if (!isJWTValid)
            return ResponseHandler.UnAuthorized<AuthenticationModel>(message: "jwt not valid");

        var userJWT = await _context.UserJWTs.RetrieveAsync(uj =>
        uj.JWT.Equals(request.RefreshTokenRequestModel.JWT) &&
        uj.RefreshJWT.Equals(request.RefreshTokenRequestModel.RefreshJWT) &&
        uj.IsRefreshJWTUsed,
        includes: new string[] { nameof(UserJWT.User) });

        if (userJWT.User is null)
            return ResponseHandler.NotFound<AuthenticationModel>();

        var authenticationModel = await _services.AuthServices.RefreshJWTAsync(userJWT.User);

        if (authenticationModel is null)
            return ResponseHandler.UnAuthorized<AuthenticationModel>(message: "jwt not active");

        return ResponseHandler.Success(authenticationModel);
    }
    #endregion
}
