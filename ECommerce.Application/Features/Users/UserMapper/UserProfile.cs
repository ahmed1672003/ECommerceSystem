

namespace ECommerce.Application.Features.Users.UserMapper;
public class UserProfile : Profile
{
    public UserProfile()
    {
        Mapp();
    }
    void Mapp()
    {
        CreateMap<PostUserDTO, User>();

        CreateMap<User, UserDTO>();
    }
}
