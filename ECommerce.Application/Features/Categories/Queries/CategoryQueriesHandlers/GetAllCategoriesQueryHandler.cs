namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;
public class GetAllCategoriesQueryHandler :
    ResponseHandler,
    IRequestHandler<GetAllCategoriesQuery, Response<IEnumerable<CategoryModel>>>

{
    public GetAllCategoriesQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<IEnumerable<CategoryModel>>>
        Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        if (!await Context.Categories.IsExistAsync(cancellationToken: cancellationToken))
            return NotFound<IEnumerable<CategoryModel>>();

        var categoriesmodels = Mapper.Map<IEnumerable<CategoryModel>>(
            await Context.Categories.RetrieveAllAsync(orderBy: e => e.Name));

        return Success(categoriesmodels);
    }
}
