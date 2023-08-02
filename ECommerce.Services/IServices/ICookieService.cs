namespace ECommerce.Services.IServices;
public interface ICookieService
{
    Task AddAsync(string key, string value);
    Task<string> RetrieveAsync(string key);
    Task DeleteAsync(string key);
}
