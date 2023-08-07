

using ECommerce.Models.Email;
using ECommerce.Services.Helpers;

using MailKit.Net.Smtp;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using MimeKit;

namespace ECommerce.Services.Services;
public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    public EmailService(IOptions<EmailSettings> options)
    {
        _emailSettings = options.Value;
    }

    public async Task<EmailModel>
        SendEmailAsync(string mailTo, string subject, string body, IReadOnlyList<IFormFile> attachments = null)
    {
        try
        {
            // build body
            var bodyBuilder = new BodyBuilder()
            {
                HtmlBody = body,
            };

            if (attachments != null)
            {
                byte[] fileBytes;

                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();

                        // add files
                        bodyBuilder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            // create email 
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_emailSettings.Sender),
                Subject = subject,
                Body = bodyBuilder.ToMessageBody(),
            };

            email.To.Add(MailboxAddress.Parse(mailTo));
            email.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Sender));

            // connect to smtp
            using var smtp = new SmtpClient();

            smtp.Connect(_emailSettings.Host, _emailSettings.Port, true);
            smtp.Authenticate(_emailSettings.Sender, _emailSettings.Password);
            var serviceMessage = await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return new()
            {
                IsSendSuccess = true,
                MailFrom = _emailSettings.Sender,
                DisplayName = _emailSettings.DisplayName,
                MailTo = mailTo,
                ServiceMessage = serviceMessage,
                Message = body,
                Subject = subject,
            };
        }
        catch
        {
            return new()
            {
                IsSendSuccess = false,
                MailFrom = _emailSettings.Sender,
                DisplayName = _emailSettings.DisplayName,
                MailTo = mailTo,
                ServiceMessage = null,
                Message = body,
                Subject = subject,
            };
        }
    }
}
