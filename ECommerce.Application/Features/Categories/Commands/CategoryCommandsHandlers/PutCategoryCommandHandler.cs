namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsHandlers;
public class PutCategoryCommandHandler :
    ResponseHandler,
    IRequestHandler<PutCategoryCommand, Response<CategoryDTO>>
{
    public PutCategoryCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<CategoryDTO>>
        Handle(PutCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Mapper.Map<Category>(request.CategoryDTO);

        try
        {
            await Context.Categories.UpdateAsync(category, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return InternalServerError<CategoryDTO>();
        }
        return Success<CategoryDTO>();
    }
}
