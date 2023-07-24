using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.Models.User.Auth;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class AddUserToRoleCommandHandler :
    ResponseHandler,
    IRequestHandler<AddUserToRoleCommand, Response<AddUserToRoleModel>>
{
    public AddUserToRoleCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<AddUserToRoleModel>>
        Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
    {
        if (
        !(await Context.Users.IsExist(u => u.Id.Equals(request.Model.UserId)))
        ||
        !(await Context.Roles.Manager.RoleExistsAsync(request.Model.RoleName)))
            return NotFound(request.Model, message: "userId or role not founded !");

        var user = await Context.Users.RetrieveAsync(u => u.Id.Equals(request.Model.UserId));

        if (await Context.Users.Manager.IsInRoleAsync(user, request.Model.RoleName))
            return BadRequest(request.Model, message: "user already in role !");

        var result = await Context.Users.Manager.AddToRoleAsync(user, request.Model.RoleName);

        if (!result.Succeeded)
            return InternalServerError(request.Model, message: "server error when add user to role process !");

        return Success(request.Model);
    }
}
