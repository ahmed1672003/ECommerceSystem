using ECommerce.Models.ResponsModels;
using ECommerce.Models.Role;

namespace ECommerce.Application.Features.Roles.Commands.RoleCommands;
public record PostRoleCommand(PostRoleModel Model) : IRequest<Response<RoleModel>>;
