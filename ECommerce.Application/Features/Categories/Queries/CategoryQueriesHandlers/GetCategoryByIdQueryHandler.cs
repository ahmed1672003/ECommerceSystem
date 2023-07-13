using ECommerce.ViewModels.ViewModels.CategoryViewModels;

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;
public class GetCategoryByIdQueryHandler :
    ResponseHandler,
    IRequestHandler<GetCategoryByIdQuery, Response<CategoryViewModel>>
{
    public GetCategoryByIdQueryHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<CategoryViewModel>>
        Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var CategoryViewModel = Mapper.Map<CategoryViewModel>(
            await Context.Categories.RetrieveAsync(c => c.Id.Equals(request.Id)));

        return Success(CategoryViewModel);
    }
}
