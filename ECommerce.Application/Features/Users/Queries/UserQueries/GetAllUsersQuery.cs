using ECommerce.ViewModels.ViewModels.UserViewModels;

namespace ECommerce.Application.Features.Users.Queries.UserQueries;
public record GetAllUsersQuery() : IRequest<Response<IEnumerable<UserViewModel>>>;
