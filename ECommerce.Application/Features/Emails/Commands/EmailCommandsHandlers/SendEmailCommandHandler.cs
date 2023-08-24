using ECommerce.Application.Features.Emails.Commands.EmailCommands;
using ECommerce.Application.ResponseServices;
using ECommerce.Models.Email;
using ECommerce.Models.ResponsModels;
using ECommerce.Services.IServices;

namespace ECommerce.Application.Features.Emails.Commands.EmailCommandsHandlers;
public class SendEmailCommandHandler :
    IRequestHandler<SendEmailCommand, Response<EmailModel>>
{
    private readonly IUnitOfServices _services;
    public SendEmailCommandHandler(
        IUnitOfServices services)
    {
        _services = services;
    }

    #region Send Email Handler
    public async Task<Response<EmailModel>>
        Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var emailModel = await _services.EmailServices.SendEmailAsync(request.Model.MailTo!, request.Model.Subject!, request.Model.Body!, request.Model.Attachments!);

        if (!emailModel.IsSendSuccess)
            return ResponseHandler.Conflict(emailModel);

        return ResponseHandler.Success(emailModel);
    }
    #endregion
}
