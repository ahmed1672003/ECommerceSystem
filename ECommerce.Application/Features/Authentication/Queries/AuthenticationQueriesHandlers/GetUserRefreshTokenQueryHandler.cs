using ECommerce.Application.Features.Authentication.Queries.AuthenticationQueries;
using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

namespace ECommerce.Application.Features.Authentication.Queries.AuthenticationQueriesHandlers;
public class GetUserRefreshTokenQueryHandler :
    ResponseHandler,
    IRequestHandler<GetUserRefreshTokenQuery, Response<UserRefreshTokenViewModel>>
{
    private readonly IAuthenticationServices _authenticationServices;
    public GetUserRefreshTokenQueryHandler(IUnitOfWork context, IMapper mapper, IAuthenticationServices authenticationServices) : base(context, mapper)
    {
        _authenticationServices = authenticationServices;
    }

    public async Task<Response<UserRefreshTokenViewModel>>
        Handle(GetUserRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        if (!await Context.UserRefreshTokens.IsExist(
            urt => urt.AccessToken.Equals(request.AccessToken)))
            return NotFound<UserRefreshTokenViewModel>();

        var model =
            await Context.UserRefreshTokens.
            RetrieveAsync(urt => urt.AccessToken.Equals(request.AccessToken));

        var viewModel = Mapper.Map<UserRefreshTokenViewModel>(model);

        return Success(viewModel);
    }
}
