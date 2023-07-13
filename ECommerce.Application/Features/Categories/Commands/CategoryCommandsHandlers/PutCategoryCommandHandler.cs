using ECommerce.ViewModels.ViewModels.CategoryViewModels;

namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsHandlers;
public class PutCategoryCommandHandler :
    ResponseHandler,
    IRequestHandler<PutCategoryCommand, Response<CategoryViewModel>>
{
    public PutCategoryCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<CategoryViewModel>>
        Handle(PutCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Mapper.Map<Category>(request.CategoryViewModel);

        try
        {
            await Context.Categories.UpdateAsync(category, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return InternalServerError<CategoryViewModel>();
        }
        return Success<CategoryViewModel>();
    }
}
