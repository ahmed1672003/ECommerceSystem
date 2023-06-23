using ECommerce.Application.Features.Users.Queries.UserQueries;

namespace ECommerce.Application.Features.Users.Queries.UserQueriesHandlers;
public class GetAllUsersQueryHandler :
    ResponseHandler,
    IRequestHandler<GetAllUsersQuery, Response<IEnumerable<UserDTO>>>
{
    public GetAllUsersQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<IEnumerable<UserDTO>>>
        Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        if (!await Context.Users.IsExist())
            return NotFound<IEnumerable<UserDTO>>();

        var dtos = Mapper.Map<IEnumerable<UserDTO>>(await Context.Users.RetrieveAllAsync());
        return Success(dtos);
    }
}
