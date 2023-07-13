using ECommerce.Application.Features.Users.Queries.UserQueries;
using ECommerce.ViewModels.ViewModels.UserViewModels;

namespace ECommerce.Application.Features.Users.Queries.UserQueriesHandlers;
public class GetAllUsersQueryHandler :
    ResponseHandler,
    IRequestHandler<GetAllUsersQuery, Response<IEnumerable<UserViewModel>>>
{
    public GetAllUsersQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<IEnumerable<UserViewModel>>>
        Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        if (!await Context.Users.IsExist())
            return NotFound<IEnumerable<UserViewModel>>();

        var models = Mapper.Map<IEnumerable<UserViewModel>>(await Context.Users.RetrieveAllAsync());
        return Success(models);
    }
}
