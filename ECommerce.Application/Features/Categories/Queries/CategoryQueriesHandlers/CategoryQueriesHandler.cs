using ECommerce.Application.Resources.Shared;

using Microsoft.Extensions.Localization;

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;
public class CategoryQueriesHandler :
    ResponseHandler,
        IRequestHandler<GetAllCategoriesQuery, Response<IEnumerable<CategoryModel>>>,
        IRequestHandler<GetCategoryByIdQuery, Response<CategoryModel>>
{
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    public CategoryQueriesHandler(IUnitOfWork context, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(context, mapper)
    {
        _stringLocalizer = stringLocalizer;
    }

    #region Get All Categories Query
    public async Task<Response<IEnumerable<CategoryModel>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        if (!await Context.Categories.IsExistAsync(cancellationToken: cancellationToken))
            return NotFound<IEnumerable<CategoryModel>>();

        var categoriesmodels = Mapper.Map<IEnumerable<CategoryModel>>(
            await Context.Categories.RetrieveAllAsync(orderBy: e => e.Name));

        return Success(categoriesmodels);
    }
    #endregion

    #region Get Category By Id Query
    public async Task<Response<CategoryModel>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {

        if (!await Context.Categories.IsExistAsync(c => c.Id == request.Id))
            return NotFound<CategoryModel>(message: _stringLocalizer[SharedResourcesKeys.NotFound]);

        var CategoryModel = Mapper.Map<CategoryModel>(
            await Context.Categories.RetrieveAsync(c => c.Id.Equals(request.Id)));

        return Success(CategoryModel);
    }

    #endregion
}
