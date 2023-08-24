using ECommerce.Application.ResponseServices;
using ECommerce.Models.ResponsModels;

namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsHandlers;
public class PutCategoryCommandHandler :
    IRequestHandler<PutCategoryCommand, Response<CategoryModel>>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;
    public PutCategoryCommandHandler(IUnitOfWork context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    #region Put Category Command Handler
    public async Task<Response<CategoryModel>> Handle(PutCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.CategoryModel);

        try
        {
            await _context.Categories.UpdateAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return ResponseHandler.Conflict<CategoryModel>();
        }
        return ResponseHandler.Success<CategoryModel>();
    }
    #endregion
}
