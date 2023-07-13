using ECommerce.Application.Features.Users.Queries.UserQueries;
using ECommerce.ViewModels.ViewModels.UserViewModels;

namespace ECommerce.Application.Features.Users.Queries.UserQueriesHandlers;
public class GetUserByIdQueryHandler :
    ResponseHandler,
    IRequestHandler<GetUserByIdQuery, Response<UserViewModel>>
{
    public GetUserByIdQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (!await Context.Users.IsExist(u => u.Id.Equals(request.Id)))
            return NotFound<UserViewModel>();

        var model = await Context.Users.Manager.FindByIdAsync(request.Id);

        var dto = Mapper.Map<UserViewModel>(model);

        return Success(dto);
    }
}
