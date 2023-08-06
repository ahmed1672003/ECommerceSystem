using ECommerce.Application.Features.Roles.Commands.RoleCommands;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.Role;

namespace ECommerce.Application.Features.Roles.Commands.RoleCommandsHandlers;
public class RoleCommandsHandler :
    ResponseHandler,
    IRequestHandler<PostRoleCommand, Response<RoleModel>>
{
    #region Fields

    #endregion

    #region CTORs
    public RoleCommandsHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    #endregion

    #region Handel Functions

    #region Post Role Command Handler
    public async Task<Response<RoleModel>>
        Handle(PostRoleCommand request, CancellationToken cancellationToken)
    {
        if (await Context.Roles.Manager.RoleExistsAsync(request.Model.Name))
            return BadRequest<RoleModel>();

        var role = Mapper.Map<Role>(request.Model);

        try
        {
            await Context.Roles.Manager.CreateAsync(role);
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
