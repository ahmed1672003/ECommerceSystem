namespace ECommerce.Application.Responses.Constants;
public class ErrorMessages
{
    public static object SuccessErrorsList = null;
    public static object NotFoundErrorsList = new List<string>() { "Not found error" };
    public static object BadRequestErrorsList = new List<string>() { "Bad request error" };
    public static object UnAuthorizedErrorsList = new List<string>() { "Un authorized error" };
    public static object InternalServerErrorsList = new List<string>() { "Internal server error" };
}