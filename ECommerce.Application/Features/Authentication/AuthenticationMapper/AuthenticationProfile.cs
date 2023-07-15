using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

namespace ECommerce.Application.Features.Authentication.AuthenticationMapper;
public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        Mapp();
    }

    private void Mapp()
    {
        CreateMap<UserRefreshToken, UserRefreshTokenViewModel>()
            .ForMember(dist => dist.TokenId, src =>
            src.MapFrom(src => src.Id));
    }
}
