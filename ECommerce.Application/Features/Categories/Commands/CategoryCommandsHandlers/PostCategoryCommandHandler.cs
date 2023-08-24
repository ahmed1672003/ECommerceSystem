using ECommerce.Application.ResponseServices;
using ECommerce.Models.ResponsModels;

namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsHandlers;
public class PostCategoryCommandHandler :
    IRequestHandler<PostCategoryCommand, Response<CategoryModel>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _context;
    public PostCategoryCommandHandler(IUnitOfWork context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    #region Post Category Command Handler
    public async Task<Response<CategoryModel>> Handle(PostCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = _mapper.Map<Category>(request.CategoryModel);
        try
        {
            await _context.Categories.CreateAsync(response, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return ResponseHandler.Conflict<CategoryModel>();
        }

        return ResponseHandler.Success<CategoryModel>();
    }
    #endregion
}
