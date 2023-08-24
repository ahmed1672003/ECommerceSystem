using ECommerce.Application.Resources.Shared;
using ECommerce.Application.ResponseServices;
using ECommerce.Models.ResponsModels;

using Microsoft.Extensions.Localization;

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;
public class GetAllCategoriesQueryHandler :

        IRequestHandler<GetAllCategoriesQuery, Response<IEnumerable<CategoryModel>>>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    public GetAllCategoriesQueryHandler(IUnitOfWork context, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
    {
        _context = context;
        _mapper = mapper;
        _stringLocalizer = stringLocalizer;
    }

    #region Get All Categories Query
    public async Task<Response<IEnumerable<CategoryModel>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        if (!await _context.Categories.IsExistAsync(cancellationToken: cancellationToken))
            return ResponseHandler.NotFound<IEnumerable<CategoryModel>>(message: _stringLocalizer[SharedResourcesKeys.NotFound]);

        var categoriesmodels = _mapper.Map<IEnumerable<CategoryModel>>(
            await _context.Categories.RetrieveAllAsync(orderBy: e => e.Name));

        return ResponseHandler.Success(categoriesmodels);
    }
    #endregion
}


