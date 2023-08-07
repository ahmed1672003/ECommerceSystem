using ECommerce.Application.Features.Roles.Commands.RoleCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.Role;

namespace ECommerce.Application.Features.Roles.Commands.RoleCommandsHandlers;
public class RoleCommandsHandlers :
    ResponseHandler,
    IRequestHandler<PostRoleCommand, Response<RoleModel>>
{
    #region Fields

    #endregion

    #region CTORs
    public RoleCommandsHandlers(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    #endregion

    #region Handel Functions

    #region Post Role Command Handler
    public async Task<Response<RoleModel>>
        Handle(PostRoleCommand request, CancellationToken cancellationToken)
    {
        var role = Mapper.Map<Role>(request.Model);
        try
        {
            var result = await Context.Roles.Manager.CreateAsync(role);

            if (!result.Succeeded)
                return BadRequest<RoleModel>(message: "role is exist", errors: result.Errors);
        }
        catch
        {
            return Conflict<RoleModel>();
        }
        var roleModel = Mapper.Map<RoleModel>(role);
        return Success(roleModel);
    }
    #endregion

    #endregion
}
