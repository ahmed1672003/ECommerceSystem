using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User.Authentication;
using ECommerce.Services.IServices;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class RegisterUserCommandHandler :
    ResponseHandler,
    IRequestHandler<RegisterUserCommand, Response<AuthenticationModel>>
{
    private readonly IUnitOfServices _services;
    public RegisterUserCommandHandler
        (
        IUnitOfWork context,
        IMapper mapper,
        IUnitOfServices services) : base(context, mapper)
    {
        _services = services;
    }
    public async Task<Response<AuthenticationModel>>
        Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = Mapper.Map<User>(request.Model);
        var result = await Context.Users.Manager.CreateAsync(user, request.Model.Password);
        // To Do: Add User To Default Roles if I need that;
        if (!result.Succeeded)
            return BadRequest<AuthenticationModel>();
        var authenticationModel = await _services.AuthServices.GetJWTAsync(user);
        return Success(authenticationModel);
    }
}
