using System.ComponentModel.DataAnnotations;

using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Application.ResponseServices;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.ResponsModels;
using ECommerce.Models.User.Authentication;
using ECommerce.Services.IServices;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class LoginUserCommandHandler :
    IRequestHandler<LoginUserCommand, Response<AuthenticationModel>>
{
    private readonly EmailAddressAttribute _emailAddressAttribute;
    private readonly IUnitOfWork _context;
    private readonly IUnitOfServices _services;
    public LoginUserCommandHandler(
        IUnitOfWork context,
        IUnitOfServices services,
        EmailAddressAttribute emailAddressAttribute)
    {
        _emailAddressAttribute = emailAddressAttribute;
        _context = context;
        _services = services;
    }

    #region Login Command Handler
    public async Task<Response<AuthenticationModel>>
        Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // check user is founded or not
        var isFounded = await _context.Users.IsExistAsync(
           u => _emailAddressAttribute.IsValid(request.Model.EmailOrUserName) ?
           u.Email.Equals(request.Model.EmailOrUserName) :
           u.UserName.Equals(request.Model.EmailOrUserName)
           );

        if (!isFounded)
            return ResponseHandler.NotFound<AuthenticationModel>();

        // select user 
        var user = await _context.Users.RetrieveAsync(
            u => u.UserName.Equals(request.Model.EmailOrUserName)
                ||
                u.Email.Equals(request.Model.EmailOrUserName),
            includes: new string[] { nameof(User.UserJWTs) });


        if (!user.EmailConfirmed)
            return ResponseHandler.BadRequest<AuthenticationModel>(message: "email does not confirmed");

        var signInResult =
            await _context.Users.SignInManager.CheckPasswordSignInAsync(user, request.Model.Password, true);


        // check password is correct or not
        if (!signInResult.Succeeded)
            return ResponseHandler.BadRequest<AuthenticationModel>(message: "make sure from email or user name or password !");

        // create new token
        var authenticationModel = await _services.AuthServices.GetJWTAsync(user);

        return ResponseHandler.Success(authenticationModel);
    }
    #endregion
}
