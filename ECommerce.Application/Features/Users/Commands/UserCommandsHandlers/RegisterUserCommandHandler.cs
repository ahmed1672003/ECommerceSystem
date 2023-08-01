using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Authentication;
using ECommerce.Services.IServices;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class RegisterUserCommandHandler :
    ResponseHandler,
    IRequestHandler<RegisterUserCommand, Response<AuthenticationModel>>
{
    private readonly IAuthenticationService _authenticationService;
    public RegisterUserCommandHandler
        (
        IUnitOfWork context,
        IMapper mapper,
        IAuthenticationService authenticationService) : base(context, mapper)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Response<AuthenticationModel>>
        Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = Mapper.Map<User>(request.Model);

        var result = await Context.Users.Manager.CreateAsync(user, request.Model.Password);

        // To Do: Add User To Default Roles if I need that;

        if (!result.Succeeded)
            return BadRequest<AuthenticationModel>();

        var authenticationModel = await _authenticationService.GetJWTAsync(user);

        return Success(authenticationModel);
    }

    #region MyRegion
    //public async Task<Response<AuthModel>>
    //    Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    //{
    //    var user = Mapper.Map<User>(request.model);

    //    var result =
    //        await Context.
    //        Users.
    //        Manager.CreateAsync(user, request.model.Password);

    //    // To Do: Add User To Default Roles if I need that;

    //    if (!result.Succeeded)
    //        return BadRequest<AuthModel>();

    //    var jwtSecurityToken = await _authService.CreateJwtTokenAsync(user);

    //    var authModel = new AuthModel()
    //    {
    //        Email = user.Email,
    //        //ExpiresOn = jwtSecurityToken.ValidTo,
    //        IsAuthenticated = true,
    //        UserName = user.UserName,
    //        Roles = await Context.Users.Manager.GetRolesAsync(user),
    //        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
    //    };

    //    if (!authModel.IsAuthenticated)
    //        return BadRequest<AuthModel>();

    //    return Success(authModel);
    //} 
    #endregion



}
