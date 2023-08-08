using ECommerce.Models.Email;

namespace ECommerce.Application.Features.Emails.Commands.EmailCommands;
public record ConfirmEmailCommand(ConfirmEmailRequestModel Model) : IRequest<Response<ConfirmEmailResponseModel>>;
