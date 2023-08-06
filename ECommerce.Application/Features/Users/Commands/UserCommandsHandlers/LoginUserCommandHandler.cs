using System.ComponentModel.DataAnnotations;

using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Authentication;
using ECommerce.Services.IServices;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class LoginUserCommandHandler :
    ResponseHandler,
    IRequestHandler<LoginUserCommand, Response<AuthenticationModel>>
{
    private readonly EmailAddressAttribute _emailAddressAttribute;
    private readonly IUnitOfServices _services;
    public LoginUserCommandHandler(IUnitOfWork context,
        EmailAddressAttribute emailAddressAttribute,
        IMapper mapper,
        IUnitOfServices services) : base(context, mapper)
    {
        _emailAddressAttribute = emailAddressAttribute;
        _services = services;
    }

    public async Task<Response<AuthenticationModel>>
        Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // check user is founded or not
        var isFounded = await Context.Users.IsExistAsync(
           u => _emailAddressAttribute.IsValid(request.Model.EmailOrUserName) ?
           u.Email.Equals(request.Model.EmailOrUserName) :
           u.UserName.Equals(request.Model.EmailOrUserName)
           );

        if (!isFounded)
            return NotFound<AuthenticationModel>();

        // select user 
        var user = await Context.Users.RetrieveAsync(
            u => u.UserName.Equals(request.Model.EmailOrUserName)
                ||
                u.Email.Equals(request.Model.EmailOrUserName),
            includes: new string[] { nameof(User.UserJWTs) });

        var signInResult =
            await Context.Users.SignInManager.CheckPasswordSignInAsync(user, request.Model.Password, false);

        // check password is correct or not
        if (!signInResult.Succeeded)
            return BadRequest<AuthenticationModel>(message: "make sure from email or user name or password !");

        // create new token
        var authenticationModel = await _services.AuthServices.GetJWTAsync(user);

        return Success(authenticationModel);
    }
}
