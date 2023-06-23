using ECommerce.Application.Features.Users.Queries.UserQueries;

namespace ECommerce.Application.Features.Users.Queries.UserQueriesHandlers;
public class GetUserByIdQueryHandler :
    ResponseHandler,
    IRequestHandler<GetUserByIdQuery, Response<UserDTO>>
{
    public GetUserByIdQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (!await Context.Users.IsExist(u => u.Id.Equals(request.Id)))
            return NotFound<UserDTO>();

        var model = await Context.Users.Manager.FindByIdAsync(request.Id);

        var dto = Mapper.Map<UserDTO>(model);

        return Success(dto);
    }
}
