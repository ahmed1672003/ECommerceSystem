using ECommerce.ViewModels.ViewModels.CategoryViewModels;

namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsHandlers;
public class PostCategoryCommandHandler :
    ResponseHandler,
    IRequestHandler<PostCategoryCommand, Response<CategoryViewModel>>
{
    public PostCategoryCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }
    public async Task<Response<CategoryViewModel>>
        Handle(PostCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = Mapper.Map<Category>(request.CategoryViewModel);
        try
        {
            await Context.Categories.CreateAsync(response, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return InternalServerError<CategoryViewModel>();
        }

        return Success<CategoryViewModel>();
    }
}
