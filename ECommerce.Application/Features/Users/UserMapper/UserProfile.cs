using ECommerce.Domain.Entities.IdentityEntities;
using ECommerce.Models.User;

namespace ECommerce.Application.Features.Users.UserMapper;
public class UserProfile : Profile
{
    public UserProfile()
    {
        Mapp();
    }

    private void Mapp()
    {
        CreateMap<PostUserModel, User>();
        CreateMap<User, UserModel>();
    }
}
