using System.ComponentModel.DataAnnotations;

using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Application.ResponseServices;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.ResponsModels;
using ECommerce.Models.User.Authentication;
using ECommerce.Services.IServices;

using Microsoft.AspNetCore.WebUtilities;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class RegisterUserCommandHandlers :

    IRequestHandler<RegisterUserCommand, Response<AuthenticationModel>>
{
    private readonly EmailAddressAttribute _emailAddressAttribute;
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfServices _services;
    private readonly IHttpContextAccessor _accessor;
    public RegisterUserCommandHandlers(
        IUnitOfWork context,
        IMapper mapper,
        IUnitOfServices services,
        IHttpContextAccessor accessor,
        EmailAddressAttribute emailAddressAttribute)
    {
        _emailAddressAttribute = emailAddressAttribute;
        _context = context;
        _mapper = mapper;
        _services = services;
        _accessor = accessor;
    }

    #region Register User Command Handler

    public async Task<Response<AuthenticationModel>>
        Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.Model);
        using var trasaction = await _context.BeginTransactionAsync();

        try
        {
            var result = await _context.Users.Manager.CreateAsync(user, request.Model.Password);

            // To Do: Add User To Default Roles if I need that;
            if (!result.Succeeded)
                return ResponseHandler.BadRequest<AuthenticationModel>(errors: result.Errors);

            var authenticationModel = await _services.AuthServices.GetJWTAsync(user);

            // email confirmation
            var token = await _context.Users.Manager.GenerateEmailConfirmationTokenAsync(user);

            var url =
            $"{_accessor.HttpContext.Request.Scheme.Trim().ToLower()}://{_accessor.HttpContext.Request.Host.ToUriComponent().Trim().ToLower()}/api/v1/Email/ConfirmEmail";

            var parameters = new Dictionary<string, string>
            {
                {"Token",token},
                { "UserId" , user.Id}
            };

            var newUrl = new Uri(QueryHelpers.AddQueryString(url, parameters));

            var emailModel =
                await _services.EmailServices.SendEmailAsync(user.Email, "Confirm your email", newUrl.ToString());

            if (!emailModel.IsSendSuccess)
                return ResponseHandler.BadRequest<AuthenticationModel>(message: "PLZ register again !");

            await trasaction.CommitAsync();

            return ResponseHandler.Success(authenticationModel);
        }
        catch
        {
            await trasaction.RollbackAsync();
            return ResponseHandler.Conflict<AuthenticationModel>();
        }
    }
    #endregion
}
