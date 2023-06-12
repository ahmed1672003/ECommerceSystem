namespace ECommerce.Application.Responses.IResponseServices;
public interface IResponseHandler
{
    public IUnitOfWork Context { get; }
    public IMapper Mapper { get; }
}
