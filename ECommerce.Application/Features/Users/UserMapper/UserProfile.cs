

using ECommerce.ViewModels.ViewModels.UserViewModels;

namespace ECommerce.Application.Features.Users.UserMapper;
public class UserProfile : Profile
{
    public UserProfile()
    {
        Mapp();
    }
    void Mapp()
    {
        CreateMap<PostUserViewModel, User>();

        CreateMap<User, UserViewModel>();
    }
}
