using ECommerce.Models.Email;
using ECommerce.Models.ResponsModels;

namespace ECommerce.Application.Features.Emails.Commands.EmailCommands;
public record ConfirmEmailCommand(ConfirmEmailRequestModel Model) : IRequest<Response<ConfirmEmailResponseModel>>;
