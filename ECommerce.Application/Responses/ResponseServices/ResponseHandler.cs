using ECommerce.Application.Constants;

namespace ECommerce.Application.Responses.ResponseServices;
public class ResponseHandler : IResponseHandler
{
    public ResponseHandler(IUnitOfWork context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public IUnitOfWork Context { get; private set; }
    public IMapper Mapper { get; private set; }
    public Response<TData> Success<TData>(
        TData data = null,
        dynamic meta = null,
        string message = null,
        object errors = null) where TData : class => new(
            statusCode: HttpStatusCode.OK,
            isSucceeded: true,
            message: message == null ? ResponseMessages.SuccessMessage : message,
            data: data,
            errors: errors == null ? ErrorMessages.SuccessErrorsList : errors,
            meta: meta);

    public Response<TData> NotFound<TData>(
        TData data = null,
        dynamic meta = null,
        string message = null,
        object errors = null) where TData : class => new(
            statusCode: HttpStatusCode.NotFound,
            isSucceeded: true,
            message: message == null ? ResponseMessages.NotFoundMessage : message,
            data: data,
            errors: errors == null ? ErrorMessages.SuccessErrorsList : errors,
            meta: meta);

    public Response<TData> BadRequest<TData>(
        TData data = null,
        dynamic meta = null,
        string message = null,
        object errors = null) where TData : class => new(
            statusCode: HttpStatusCode.BadRequest,
            isSucceeded: false,
            message: message == null ? ResponseMessages.BadRequestMessage : message,
            data: data,
            errors: errors == null ? ErrorMessages.SuccessErrorsList : errors,
            meta: meta);

    public Response<TData> UnAuthorized<TData>(
        TData data = null,
        dynamic meta = null,
        string message = null,
        List<dynamic> errors = null) where TData : class =>
        new(
            statusCode: HttpStatusCode.Unauthorized,
            isSucceeded: false,
            message: message == null ? ResponseMessages.UnAuthorizedMessage : message,
            data: data,
            errors: errors == null ? ErrorMessages.SuccessErrorsList : errors,
            meta: meta);

    public Response<TData> InternalServerError<TData>(
        TData data = null,
        dynamic meta = null,
        string message = null,
        List<dynamic> errors = null) where TData : class => new(
            statusCode: HttpStatusCode.InternalServerError,
            isSucceeded: false,
            message: message == null ? ResponseMessages.InternalServerErrorMessage : message,
            data: data,
            errors: errors == null ? ErrorMessages.InternalServerErrorsList : errors,
            meta: meta);

    public Response<TData> Conflict<TData>(
        TData data = null,
        dynamic meta = null,
        string message = null,
        List<dynamic> errors = null) where TData : class => new(
            statusCode: HttpStatusCode.Conflict,
            isSucceeded: false,
            message: message == null ? ResponseMessages.ConflictErrorMessage : message,
            data: data,
            errors: errors == null ? ErrorMessages.ConflictErrorList : errors,
            meta: meta);
}
