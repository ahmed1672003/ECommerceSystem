
using ECommerce.ViewModels.ViewModels.CategoryViewModels;

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;
public class GetAllCategoriesQueryHandler :
    ResponseHandler,
    IRequestHandler<GetAllCategoriesQuery, Response<IEnumerable<CategoryViewModel>>>

{
    public GetAllCategoriesQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<IEnumerable<CategoryViewModel>>>
        Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        if (!await Context.Categories.IsExist(cancellationToken: cancellationToken))
            return NotFound<IEnumerable<CategoryViewModel>>();

        var categoriesDTOs = Mapper.Map<IEnumerable<CategoryViewModel>>(
            await Context.Categories.RetrieveAllAsync(orderBy: e => e.Name));

        return Success(categoriesDTOs);
    }
}
