using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace ECommerce.Models.Email;
public class SendEmailModel
{
    public string? Subject { get; set; }

    [EmailAddress]
    public string? MailTo { get; set; }
    public string? Body { get; set; }
    public IReadOnlyList<IFormFile>? Attachments { get; set; }
}
