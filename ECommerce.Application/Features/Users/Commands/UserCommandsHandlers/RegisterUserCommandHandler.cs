using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Authentication;
using ECommerce.Services.IServices;

using Microsoft.AspNetCore.WebUtilities;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class RegisterUserCommandHandler :
    ResponseHandler,
    IRequestHandler<RegisterUserCommand, Response<AuthenticationModel>>
{
    private readonly IUnitOfServices _services;
    private readonly IHttpContextAccessor _accessor;
    public RegisterUserCommandHandler
        (
        IUnitOfWork context,
        IMapper mapper,
        IUnitOfServices services,
        IHttpContextAccessor accessor) : base(context, mapper)
    {
        _services = services;
        _accessor = accessor;
    }
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
}
