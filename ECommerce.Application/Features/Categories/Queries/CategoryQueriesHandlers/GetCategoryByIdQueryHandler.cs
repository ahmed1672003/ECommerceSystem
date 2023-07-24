namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;
public class GetCategoryByIdQueryHandler :
    ResponseHandler,
    IRequestHandler<GetCategoryByIdQuery, Response<CategoryModel>>
{
    public GetCategoryByIdQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<CategoryModel>>
        Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var CategoryModel = Mapper.Map<CategoryModel>(
            await Context.Categories.RetrieveAsync(c => c.Id.Equals(request.Id)));

        return Success(CategoryModel);
    }
}
