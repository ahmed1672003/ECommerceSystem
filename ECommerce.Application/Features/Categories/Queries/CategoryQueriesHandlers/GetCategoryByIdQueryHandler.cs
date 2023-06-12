namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;
public class GetCategoryByIdQueryHandler :
    ResponseHandler,
    IRequestHandler<GetCategoryByIdQuery, Response<CategoryDTO>>
{
    public GetCategoryByIdQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<CategoryDTO>>
        Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryDTO = Mapper.Map<CategoryDTO>(
            await Context.Categories.RetrieveAsync(c => c.Id.Equals(request.Id)));

        return Success(categoryDTO);
    }
}
