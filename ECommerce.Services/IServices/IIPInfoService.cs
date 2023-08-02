namespace ECommerce.Services.IServices;
public interface IIPInfoService
{
    Task<string> RetrieveIPAddressAsync(int index = 0);
}
