using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.Role;

namespace ECommerce.Application.Features.Roles.RoleMapper;
public class RoleProfile : Profile
{
    public RoleProfile()
    {
        Mapp();
    }

    void Mapp()
    {
        CreateMap<PostRoleModel, Role>().ReverseMap();
        CreateMap<Role, RoleModel>().ReverseMap();
    }
}
