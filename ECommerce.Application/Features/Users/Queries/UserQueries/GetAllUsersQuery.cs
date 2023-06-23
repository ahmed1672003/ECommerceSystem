namespace ECommerce.Application.Features.Users.Queries.UserQueries;
public record GetAllUsersQuery() : IRequest<Response<IEnumerable<UserDTO>>>;
