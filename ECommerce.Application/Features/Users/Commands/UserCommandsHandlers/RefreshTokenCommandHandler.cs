using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Authentication;
using ECommerce.Services.Interfaces;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class RefreshTokenCommandHandler :
    ResponseHandler,
    IRequestHandler<RefreshTokenCommand, Response<AuthenticationModel>>
{
    private readonly IAuthenticationService _authenticationService;
    public RefreshTokenCommandHandler(
        IUnitOfWork context,
        IMapper mapper,
        IAuthenticationService authenticationService) : base(context, mapper)
    {
        _authenticationService = authenticationService;
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
            await _authenticationService.ReadJWTAsync(request.RefreshTokenRequestModel.JWT);

        var isJWTValid = await _authenticationService
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

        var authenticationModel = await _authenticationService.RefreshJWTAsync(userJWT.User);

        if (authenticationModel is null)
            return UnAuthorized<AuthenticationModel>(message: "jwt not active");

        return Success(authenticationModel);
    }
}
