namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;

public class GetAllCategoriesPaginatedQueryHandler :
    PaginationResponseHandler,
    IRequestHandler<GetAllCategoriesPaginatedQuery, PaginationResponse<IEnumerable<CategoryDTO>>>
{
    public GetAllCategoriesPaginatedQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<PaginationResponse<IEnumerable<CategoryDTO>>>
        Handle(GetAllCategoriesPaginatedQuery request, CancellationToken cancellationToken)
    {
        if (!await Context.Categories.IsExist())
            return NotFound<IEnumerable<CategoryDTO>>();

        var result = Mapper.Map<IEnumerable<CategoryDTO>>(
            await Context.Categories.RetrieveAllAsync(
                orderBy: e => e.Name,
                paginationOn: true,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken));

        return Success(
            result,
            count: await Context.Categories.CountAsync(),
            currentPage: request.PageNumber!.Value,
            pageSize: request.PageSize!.Value);
    }
}
