using ECommerce.Application.Resources.Shared;
using ECommerce.Application.ResponseServices;
using ECommerce.Models.ResponsModels;

using Microsoft.Extensions.Localization;

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesHandlers;
public class GetCategoryByIdQueryHandler :
        IRequestHandler<GetCategoryByIdQuery, Response<CategoryModel>>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    public GetCategoryByIdQueryHandler(IUnitOfWork context, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
    {
        _context = context;
        _mapper = mapper;
        _stringLocalizer = stringLocalizer;
    }

    #region Get Category By Id Query
    public async Task<Response<CategoryModel>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {

        if (!await _context.Categories.IsExistAsync(c => c.Id == request.Id))
            return ResponseHandler.NotFound<CategoryModel>(message: _stringLocalizer[SharedResourcesKeys.NotFound]);

        var CategoryModel = _mapper.Map<CategoryModel>(
            await _context.Categories.RetrieveAsync(c => c.Id.Equals(request.Id)));

        return ResponseHandler.Success(CategoryModel);
    }

    #endregion
}
