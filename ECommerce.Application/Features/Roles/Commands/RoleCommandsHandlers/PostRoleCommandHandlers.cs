using ECommerce.Application.Features.Roles.Commands.RoleCommands;
using ECommerce.Application.ResponseServices;
using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.ResponsModels;
using ECommerce.Models.Role;

namespace ECommerce.Application.Features.Roles.Commands.RoleCommandsHandlers;
public class PostRoleCommandHandlers :

    IRequestHandler<PostRoleCommand, Response<RoleModel>>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;
    #region Fields

    #endregion

    #region CTORs
    public PostRoleCommandHandlers(IUnitOfWork context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    #endregion

    #region Handel Functions

    #region Post Role Command Handler
    public async Task<Response<RoleModel>>
        Handle(PostRoleCommand request, CancellationToken cancellationToken)
    {
        var role = _mapper.Map<Role>(request.Model);
        try
        {
            var result = await _context.Roles.Manager.CreateAsync(role);

            if (!result.Succeeded)
                return ResponseHandler.BadRequest<RoleModel>(message: "role is exist", errors: result.Errors);
        }
        catch
        {
            return ResponseHandler.Conflict<RoleModel>();
        }
        var roleModel = _mapper.Map<RoleModel>(role);
        return ResponseHandler.Success(roleModel);
    }
    #endregion

    #endregion
}
