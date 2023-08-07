using ECommerce.Models.Email;

namespace ECommerce.Application.Features.Emails.Commands.EmailCommands;
public record SendEmailCommand(SendEmailModel Model) : IRequest<Response<EmailModel>>;
