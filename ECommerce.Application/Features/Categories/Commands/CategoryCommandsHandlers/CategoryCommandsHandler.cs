namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsHandlers;
public class CategoryCommandsHandler : ResponseHandler,
    IRequestHandler<PostCategoryCommand, Response<CategoryModel>>,
    IRequestHandler<PutCategoryCommand, Response<CategoryModel>>
{
    public CategoryCommandsHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper)
    {
    }

    #region Post Category Command Handler
    public async Task<Response<CategoryModel>> Handle(PostCategoryCommand request, CancellationToken cancellationToken)
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
    #endregion


    #region Put Category Command Handler
    public async Task<Response<CategoryModel>> Handle(PutCategoryCommand request, CancellationToken cancellationToken)
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
    #endregion


}
