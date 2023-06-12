using System.Net;

namespace ECommerce.Application.Responses.ResponseServices;
public class Response<TData> where TData : class
{
    public Response(
        HttpStatusCode statusCode = default,
        bool isSucceeded = default,
        TData? data = default,
        dynamic meta = default,
        string message = default,
        List<string> errors = default)
    {
        StatusCode = statusCode;
        IsSucceeded = isSucceeded;
        Data = data;
        Meta = meta;
        Message = message;
        Errors = errors;
    }

    public HttpStatusCode StatusCode { get; set; }
    public bool IsSucceeded { get; set; }
    public TData? Data { get; set; }
    public dynamic Meta { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}