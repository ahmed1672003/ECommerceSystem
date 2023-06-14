namespace ECommerce.Application.Responses.ResponseServices;
public class PaginationResponse<TData> : Response<TData> where TData : class
{
    public PaginationResponse(
        HttpStatusCode statusCode = default,
        bool isSucceeded = default,
        TData? data = default,
        object meta = default,
        string message = default,
        object errors = default,
        int count = 0,
        int page = 1,
        int pageSize = 10
        ) : base(statusCode, isSucceeded, data, meta, message, errors)
    {
        PageSize = pageSize;
        TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count / pageSize)));
        TotalCount = count;
        CurrentPage = page;
    }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public bool MoveNext => CurrentPage > 1;
    public bool MovePrevious => CurrentPage < TotalPages;
}
