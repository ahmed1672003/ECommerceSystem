using ECommerce.Models.Email;

using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.IServices;
public interface IEmailService
{
    Task<EmailModel> SendEmailAsync(string mailTo, string subject, string body, IReadOnlyList<IFormFile> attachments = null);
    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(string? userId, string? code);
}
