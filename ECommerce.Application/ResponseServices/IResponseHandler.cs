namespace ECommerce.Application.ResponseServices;
public interface IResponseHandler
{
    public IUnitOfWork Context { get; }
    public IMapper Mapper { get; }
}
