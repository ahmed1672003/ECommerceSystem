

namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsHandlers;
public class PostCategoryCommandHandler :
    ResponseHandler,
    IRequestHandler<PostCategoryCommand, Response<GetCategoryDTO>>
{
    public PostCategoryCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }
    public async Task<Response<GetCategoryDTO>>
        Handle(PostCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = Mapper.Map<Category>(request.CategoryDTO);


        try
        {
            await Context.Categories.CreateAsync(response);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return InternalServerError<GetCategoryDTO>();
        }

        return Success<GetCategoryDTO>();
    }
}
