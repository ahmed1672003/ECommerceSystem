using System.Data.Common;

using ECommerce.Application.Constants;

namespace ECommerce.Application.MiddleWares;
public class ErrorHandlerMiddleWare
{
    private readonly RequestDelegate _next;
    public ErrorHandlerMiddleWare(RequestDelegate next) => _next = next;
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = ContentTypes.ApplicationOverJSON;
            var responseModel = new Response<string>() { IsSucceeded = false, Message = error.Message };
            //TODO:: cover all validation errors
            switch (error)
            {
                case UnauthorizedAccessException e:
                    // custom application error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.Unauthorized;
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);
                    break;

                case ValidationException e:
                    // custom validation error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.UnprocessableEntity);
                    break;
                case KeyNotFoundException e:
                    // not found error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.NotFound;
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                    break;

                case DbUpdateException e:
                    // can't update error
                    responseModel.Message = e.Message;
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                    break;
                case DbException e:

                    responseModel.Message = e.Message;
                    responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                    break;
                default:
                    // unhandled error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}