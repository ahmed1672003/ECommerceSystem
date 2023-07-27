using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Auth;
using ECommerce.Services.Interfaces;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class LoginUserCommandHandler :
    ResponseHandler,
    IRequestHandler<LoginUserCommand, Response<AuthModel>>
{
    private readonly IAuthService _authService;
    private readonly EmailAddressAttribute _emailAddressAttribute;
    public LoginUserCommandHandler(IUnitOfWork context,
        IMapper mapper, 
        IAuthService authService,
        EmailAddressAttribute emailAddressAttribute) : base(context, mapper)
    {
        _authService = authService;
        _emailAddressAttribute = emailAddressAttribute;
    }

    public async Task<Response<AuthModel>>
        Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // check user is founded or not
        var isFounded = await Context.Users.IsExist(
            u => _emailAddressAttribute.IsValid(request.Model.EmailOrUserName) ?
            u.Email.Equals(request.Model.EmailOrUserName) :
            u.UserName.Equals(request.Model.EmailOrUserName)
            );

        if (!isFounded)
            return NotFound<AuthModel>();

        // select user 
        var user = await Context.Users.RetrieveAsync(
            u => u.UserName.Equals(request.Model.EmailOrUserName)
            ||
             u.Email.Equals(request.Model.EmailOrUserName),
            includes: new string[] { nameof(User.UserRefreshTokens) });

        // check password is correct or not
        if (!await Context.Users.Manager.CheckPasswordAsync(user, request.Model.Password))
            return BadRequest<AuthModel>(message: "email or password is not correct!");

        // create new token
        var jwtSecurityToken = await _authService.CreateJwtTokenAsync(user);

        // initialize AuthModel
        var authModel = new AuthModel()
        {
            UserName = user.UserName,
            Email = user.Email,
            //ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = await Context.Users.Manager.GetRolesAsync(user),
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
        };

        // check if user have any active token
        if (user.UserRefreshTokens.Any(t => t.IsActive))
        {
            var activeUserRefreshToken = user.UserRefreshTokens.FirstOrDefault(t => t.IsActive);
            authModel.RefreshToken = activeUserRefreshToken.Token;
            authModel.RefreshTokenExpiration = activeUserRefreshToken.ExpiresOn;
        }
        else
        {
            var userRefreshToken = await _authService.GenerateRefreshTokenAsync();
            authModel.RefreshToken = userRefreshToken.Token;
            authModel.RefreshTokenExpiration = userRefreshToken.ExpiresOn;
            user.UserRefreshTokens.Add(userRefreshToken);
            try
            {
                await Context.Users.Manager.UpdateAsync(user);
            }
            catch
            {
                return InternalServerError(authModel, message: "Something wrong when save refresh token in database");
            }
        }
        return Success(authModel);
    }
}
