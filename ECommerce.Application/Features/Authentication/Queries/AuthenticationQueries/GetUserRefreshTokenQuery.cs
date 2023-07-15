using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

namespace ECommerce.Application.Features.Authentication.Queries.AuthenticationQueries;
public record GetUserRefreshTokenQuery(string AccessToken) : IRequest<Response<UserRefreshTokenViewModel>>;


