namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsHandlers;
public class PostCategoryCommandHandler :
    ResponseHandler,
    IRequestHandler<PostCategoryCommand, Response<CategoryModel>>
{
    public PostCategoryCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }
    public async Task<Response<CategoryModel>>
        Handle(PostCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = Mapper.Map<Category>(request.CategoryModel);
        try
        {
            await Context.Categories.CreateAsync(response, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return InternalServerError<CategoryModel>();
        }

        return Success<CategoryModel>();
    }
}
