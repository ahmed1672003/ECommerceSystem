using System.Linq.Expressions;

using ECommerce.Application.ResponseServices;
using ECommerce.Models.ResponsModels;

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;

public class CategoriesPaginationQueryHandler :
    IRequestHandler<CategoriesPaginationQuery, PaginationResponse<IEnumerable<CategoryModel>>>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;

    public CategoriesPaginationQueryHandler(IUnitOfWork context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<IEnumerable<CategoryModel>>>
        Handle(CategoriesPaginationQuery request, CancellationToken cancellationToken)
    {
        if (!await _context.Categories.IsExistAsync())
            return PaginationResponseHandler.NotFound<IEnumerable<CategoryModel>>();

        Expression<Func<Category, object>> orderBy = (e) => new();

        switch (request.OrderBy)
        {
            case CategoryOrderBy.CategoryName:
                orderBy = (c) => c.Name;
                break;
            default:
                orderBy = (c) => c.Id;
                break;
        }

        var result = _mapper.Map<IEnumerable<CategoryModel>>(
            await _context.Categories.RetrieveAllAsync(
                orderBy: orderBy,
                paginationOn: true,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken));

        return PaginationResponseHandler.Success(
            result,
            count: await _context.Categories.CountAsync(),
            currentPage: request.PageNumber!.Value,
            pageSize: request.PageSize!.Value);
    }
}
