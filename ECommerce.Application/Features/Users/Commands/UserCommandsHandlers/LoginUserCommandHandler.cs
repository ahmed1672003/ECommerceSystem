using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Models.User.Auth;
using ECommerce.Services.Interfaces;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class LoginUserCommandHandler :
    ResponseHandler,
    IRequestHandler<LoginUserCommand, Response<AuthModel>>
{
    private readonly IAuthService _authService;
    public LoginUserCommandHandler(IUnitOfWork context, IMapper mapper, IAuthService authService) : base(context, mapper)
    {
        _authService = authService;
    }

    public async Task<Response<AuthModel>>
        Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var isFounded = await Context.Users.IsExist(
            u => new EmailAddressAttribute().IsValid(request.Model.EmailOrUserName) ?
            u.Email.Equals(request.Model.EmailOrUserName) :
            u.UserName.Equals(request.Model.EmailOrUserName)
            );

        if (!isFounded)
            return NotFound<AuthModel>();

        var user = await Context.Users.RetrieveAsync(
            u => u.UserName.Equals(request.Model.EmailOrUserName)
            ||
             u.Email.Equals(request.Model.EmailOrUserName));

        if (!await Context.Users.Manager.CheckPasswordAsync(user, request.Model.Password))
            return BadRequest<AuthModel>(message: "email or password is not correct!");


        var jwtSecurityToken = await _authService.CreateJwtTokenAsync(user);

        var authModel = new AuthModel()
        {
            UserName = user.UserName,
            Email = user.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = await Context.Users.Manager.GetRolesAsync(user),
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
        };

        return Success(authModel);
    }
}
