using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Authentication;
using ECommerce.Services.IServices;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class RefreshTokenCommandHandler :
    ResponseHandler,
    IRequestHandler<RefreshTokenCommand, Response<AuthenticationModel>>
{
    private readonly IUnitOfServices _services;

    public RefreshTokenCommandHandler(
        IUnitOfWork context,
        IMapper mapper,
        IAuthenticationService authenticationService,
        IUnitOfServices services) : base(context, mapper)
    {
        _services = services;
    }

    public async Task<Response<AuthenticationModel>>
        Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (!await Context.UserJWTs.IsExist(uj =>
        uj.JWT.Equals(request.RefreshTokenRequestModel.JWT) &&
        uj.RefreshJWT.Equals(request.RefreshTokenRequestModel.RefreshJWT) &&
        uj.IsRefreshJWTUsed))
            return NotFound<AuthenticationModel>();

        var jwtSecurityToken =
            await _services.AuthServices.ReadJWTAsync(request.RefreshTokenRequestModel.JWT);

        var isJWTValid = await _services.AuthServices
            .IsJWTValid.Invoke(request.RefreshTokenRequestModel.JWT, jwtSecurityToken);

        if (!isJWTValid)
            return UnAuthorized<AuthenticationModel>(message: "jwt not valid");

        var userJWT = await Context.UserJWTs.RetrieveAsync(uj =>
        uj.JWT.Equals(request.RefreshTokenRequestModel.JWT) &&
        uj.RefreshJWT.Equals(request.RefreshTokenRequestModel.RefreshJWT) &&
        uj.IsRefreshJWTUsed,
        includes: new string[] { nameof(UserJWT.User) });

        if (userJWT.User is null)
            return NotFound<AuthenticationModel>();

        var authenticationModel = await _services.AuthServices.RefreshJWTAsync(userJWT.User);

        if (authenticationModel is null)
            return UnAuthorized<AuthenticationModel>(message: "jwt not active");

        return Success(authenticationModel);
    }
}
