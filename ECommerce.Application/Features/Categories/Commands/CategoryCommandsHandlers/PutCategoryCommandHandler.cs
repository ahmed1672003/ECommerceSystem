namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsHandlers;
public class PutCategoryCommandHandler :
    ResponseHandler,
    IRequestHandler<PutCategoryCommand, Response<CategoryModel>>
{
    public PutCategoryCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<CategoryModel>>
        Handle(PutCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Mapper.Map<Category>(request.CategoryModel);

        try
        {
            await Context.Categories.UpdateAsync(category, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return InternalServerError<CategoryModel>();
        }
        return Success<CategoryModel>();
    }
}
