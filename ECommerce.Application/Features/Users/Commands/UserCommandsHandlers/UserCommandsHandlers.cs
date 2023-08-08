using System.ComponentModel.DataAnnotations;

using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Authentication;
using ECommerce.Services.IServices;

using Microsoft.AspNetCore.WebUtilities;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class UserCommandsHandlers : ResponseHandler,
    IRequestHandler<LoginUserCommand, Response<AuthenticationModel>>,
    IRequestHandler<RefreshTokenCommand, Response<AuthenticationModel>>,
    IRequestHandler<RegisterUserCommand, Response<AuthenticationModel>>
{
    private readonly EmailAddressAttribute _emailAddressAttribute;
    private readonly IUnitOfServices _services;
    private readonly IHttpContextAccessor _accessor;
    public UserCommandsHandlers(
        IUnitOfWork context,
        IMapper mapper,
        IUnitOfServices services,
        IHttpContextAccessor accessor,
        EmailAddressAttribute emailAddressAttribute) : base(context, mapper)
    {
        _emailAddressAttribute = emailAddressAttribute;
        _services = services;
        _accessor = accessor;
    }

    #region Login Command Handler
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

        if (!user.EmailConfirmed)
            return BadRequest<AuthenticationModel>(message: "email does not confirmed");

        // check password is correct or not
        if (!signInResult.Succeeded)
            return BadRequest<AuthenticationModel>(message: "make sure from email or user name or password !");

        // create new token
        var authenticationModel = await _services.AuthServices.GetJWTAsync(user);

        return Success(authenticationModel);
    }
    #endregion

    #region Refresh Token Command Handler
    public async Task<Response<AuthenticationModel>>
        Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (!await Context.UserJWTs.IsExistAsync(uj =>
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
    #endregion

    #region Register User Command Handler

    public async Task<Response<AuthenticationModel>>
        Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = Mapper.Map<User>(request.Model);
        using var trasaction = Context.Transaction;

        try
        {
            var result = await Context.Users.Manager.CreateAsync(user, request.Model.Password);

            // To Do: Add User To Default Roles if I need that;
            if (!result.Succeeded)
                return BadRequest<AuthenticationModel>(errors: result.Errors);

            var authenticationModel = await _services.AuthServices.GetJWTAsync(user);

            // email confirmation
            var code = await Context.Users.Manager.GenerateEmailConfirmationTokenAsync(user);

            var url =
            $"{_accessor.HttpContext.Request.Scheme.Trim().ToLower()}://{_accessor.HttpContext.Request.Host.ToUriComponent().Trim().ToLower()}/api/v1/Email/ConfirmEmail";

            var parameters = new Dictionary<string, string>
            {
                {"Code",code},
                { "UserId" , user.Id}
            };

            var newUrl = new Uri(QueryHelpers.AddQueryString(url, parameters));

            var emailModel =
                await _services.EmailServices.SendEmailAsync(user.Email, "Confirm your email", newUrl.ToString());

            if (!emailModel.IsSendSuccess)
                return BadRequest<AuthenticationModel>(message: "PLZ register again !");

            await trasaction.CommitAsync();

            return Success(authenticationModel);
        }
        catch
        {
            await trasaction.RollbackAsync();
            return Conflict<AuthenticationModel>();
        }
    }
    #endregion
}
