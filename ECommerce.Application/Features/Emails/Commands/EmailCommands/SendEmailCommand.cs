using ECommerce.Models.Email;
using ECommerce.Models.ResponsModels;

namespace ECommerce.Application.Features.Emails.Commands.EmailCommands;
public record SendEmailCommand(SendEmailModel Model) : IRequest<Response<EmailModel>>;
