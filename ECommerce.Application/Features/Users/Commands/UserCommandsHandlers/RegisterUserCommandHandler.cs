using System.IdentityModel.Tokens.Jwt;

using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Auth;
using ECommerce.Services.Interfaces;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class RegisterUserCommandHandler :
    ResponseHandler,
    IRequestHandler<RegisterUserCommand, Response<AuthModel>>
{
    private readonly IAuthService _authService;
    public RegisterUserCommandHandler(IUnitOfWork context, IMapper mapper, IAuthService authService) : base(context, mapper)
    {
        _authService = authService;
    }

    public async Task<Response<AuthModel>>
        Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = Mapper.Map<User>(request.model);

        var result =
            await Context.
            Users.
            Manager.CreateAsync(user, request.model.Password);

        // To Do: Add User To Default Roles if I need that;

        if (!result.Succeeded)
            return BadRequest<AuthModel>();

        var jwtSecurityToken = await _authService.CreateJwtTokenAsync(user);

        var authModel = new AuthModel()
        {
            Email = user.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            UserName = user.UserName,
            Roles = await Context.Users.Manager.GetRolesAsync(user),
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
        };

        if (!authModel.IsAuthenticated)
            return BadRequest<AuthModel>();

        return Success(authModel);
    }
}
