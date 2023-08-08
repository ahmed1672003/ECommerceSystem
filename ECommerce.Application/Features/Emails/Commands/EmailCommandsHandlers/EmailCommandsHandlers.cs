using ECommerce.Application.Features.Emails.Commands.EmailCommands;
using ECommerce.Models.Email;
using ECommerce.Services.IServices;

namespace ECommerce.Application.Features.Emails.Commands.EmailCommandsHandlers;
public class EmailCommandsHandlers :
    ResponseHandler,
    IRequestHandler<SendEmailCommand, Response<EmailModel>>,
    IRequestHandler<ConfirmEmailCommand, Response<ConfirmEmailResponseModel>>
{
    private readonly IUnitOfServices _services;
    public EmailCommandsHandlers(
        IUnitOfWork context,
        IMapper mapper,
        IUnitOfServices services) : base(context, mapper)
    {
        _services = services;
    }

    #region Send Email Handler
    public async Task<Response<EmailModel>>
        Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var emailModel = await _services.EmailServices.SendEmailAsync(request.Model.MailTo!, request.Model.Subject!, request.Model.Body!, request.Model.Attachments!);

        if (!emailModel.IsSendSuccess)
            return Conflict(emailModel);

        return Success(emailModel);
    }
    #endregion

    #region Confirm Email Handler

    public async Task<Response<ConfirmEmailResponseModel>>
        Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var model = await _services.EmailServices.ConfirmEmailAsync(request.Model.UserId, request.Model.Code);

        if (!model.IsEmailConfirmed)
            return Conflict(model);

        return Success(model);
    }
    #endregion
}
