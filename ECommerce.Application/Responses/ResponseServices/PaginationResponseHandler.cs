namespace ECommerce.Application.Responses.ResponseServices;
public class PaginationResponseHandler : IResponseHandler
{
    public PaginationResponseHandler(IUnitOfWork context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
    public IUnitOfWork Context { get; private set; }
    public IMapper Mapper { get; private set; }
    public PaginationResponse<TData> Success<TData>(
       TData data = null,
       dynamic meta = null,
       string message = null,
       object errors = null,
       int count = 0,
       int page = 1,
       int pageSize = 10
        ) where TData : class => new(
           statusCode: HttpStatusCode.OK,
           isSucceeded: true,
           message: message == null ? ResponseMessages.SuccessMessage : message,
           data: data,
           errors: errors == null ? ErrorMessages.SuccessErrorsList : errors,
           meta: meta,
           count: count,
           page: page,
           pageSize: pageSize);
}
