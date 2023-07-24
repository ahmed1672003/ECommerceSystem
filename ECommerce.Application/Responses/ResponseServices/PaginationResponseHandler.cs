using ECommerce.Application.Constants;

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
       int currentPage = 1,
       int pageSize = 10
        ) where TData : class => new(
           statusCode: HttpStatusCode.OK,
           isSucceeded: true,
           message: message == null ? ResponseMessages.SuccessMessage : message,
           data: data,
           errors: errors == null ? ErrorMessages.SuccessErrorsList : errors,
           meta: meta,
           count: count,
           currentPage: currentPage,
           pageSize: pageSize);

    public PaginationResponse<TData> NotFound<TData>(
      TData data = null,
      dynamic meta = null,
      string message = null,
      object errors = null,
      int count = 0,
      int currentPage = 1,
      int pageSize = 10
       ) where TData : class => new(
          statusCode: HttpStatusCode.OK,
          isSucceeded: true,
          message: message == null ? ResponseMessages.NotFoundMessage : message,
          data: data,
          errors: errors == null ? ErrorMessages.NotFoundErrorsList : errors,
          meta: meta,
          count: count,
          currentPage: currentPage,
          pageSize: pageSize);
}
