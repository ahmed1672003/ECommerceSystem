namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsHandlers;
public class PostCategoryCommandHandler :
    ResponseHandler,
    IRequestHandler<PostCategoryCommand, Response<CategoryDTO>>
{
    public PostCategoryCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }
    public async Task<Response<CategoryDTO>>
        Handle(PostCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = Mapper.Map<Category>(request.CategoryDTO);
        try
        {
            await Context.Categories.CreateAsync(response, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return InternalServerError<CategoryDTO>();
        }

        return Success<CategoryDTO>();
    }
}
