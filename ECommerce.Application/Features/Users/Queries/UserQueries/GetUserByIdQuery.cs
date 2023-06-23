namespace ECommerce.Application.Features.Users.Queries.UserQueries;
public record GetUserByIdQuery(string Id) : IRequest<Response<UserDTO>>;

