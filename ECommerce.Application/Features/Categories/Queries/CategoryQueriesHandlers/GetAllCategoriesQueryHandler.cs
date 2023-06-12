
namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;
public class GetAllCategoriesQueryHandler :
    ResponseHandler,
    IRequestHandler<GetAllCategoriesQuery, Response<IEnumerable<CategoryDTO>>>

{
    public GetAllCategoriesQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<IEnumerable<CategoryDTO>>>
        Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        if (!await Context.Categories.IsExist(cancellationToken: cancellationToken))
            return NotFound<IEnumerable<CategoryDTO>>();

        var categoriesDTOs = Mapper.Map<IEnumerable<CategoryDTO>>(await Context.Categories.RetrieveAllAsync());

        return Success(categoriesDTOs);
    }
}
