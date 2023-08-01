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
        // To Do: validate request JWT & RefreshJWT

        var jwtSecurityToken =
            await _authenticationService.ReadJWTAsync(request.RefreshTokenRequestModel.JWT);

        var isJWTValid = await _authenticationService
            .IsJWTValid.Invoke(request.RefreshTokenRequestModel.JWT, jwtSecurityToken);


        if (!isJWTValid)
            return UnAuthorized<AuthenticationModel>(message: "jwt not valid");

        var user = await Context.Users.RetrieveAsync(u =>
                    u.UserJWTs.Any(uj =>
                                    uj.JWT.Equals(request.RefreshTokenRequestModel.JWT) &&
                                    uj.RefreshJWT.Equals(request.RefreshTokenRequestModel.RefreshJWT) &&
                                    uj.IsRefreshJWTUsed && uj.IsRefreshJWTActive),
                                    includes: new string[] { nameof(User.UserJWTs) });

        if (user == null)
            return NotFound<AuthenticationModel>();

        var authenticationModel = await _authenticationService.RefreshJWTAsync(user);

        if (authenticationModel is null)
            return BadRequest<AuthenticationModel>();

        return Success(authenticationModel);
    }
    #region MyRegion

    //public async Task<Response<AuthModel>> 
    //    Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    //{
    //    if(string.IsNullOrEmpty(request.RefreshToken) || string.IsNullOrWhiteSpace(request.RefreshToken))
    //         return BadRequest(new AuthModel());

    //    var authModel = await _authService.RefreshTokenAsync(request.RefreshToken);

    //    if (!authModel.IsAuthenticated)
    //        return BadRequest(authModel);

    //    return Success(authModel);
    //} 
    #endregion




}
