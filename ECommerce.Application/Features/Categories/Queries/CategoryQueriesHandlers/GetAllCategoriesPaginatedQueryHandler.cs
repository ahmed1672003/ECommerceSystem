using System.Linq.Expressions;

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

        Expression<Func<Category, object>> orderBy = (e) => new();

        switch (request.OrderBy)
        {
            case CategoryEnum.CategoryName:
                orderBy = (c) => c.Name;
                break;
            default:
                orderBy = (c) => c.Id;
                break;
        }

        var result = Mapper.Map<IEnumerable<CategoryDTO>>(
            await Context.Categories.RetrieveAllAsync(
                orderBy: orderBy,
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
