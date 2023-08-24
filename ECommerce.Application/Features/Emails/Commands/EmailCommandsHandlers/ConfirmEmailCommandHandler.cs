using ECommerce.Application.Features.Emails.Commands.EmailCommands;
using ECommerce.Application.ResponseServices;
using ECommerce.Models.Email;
using ECommerce.Models.ResponsModels;
using ECommerce.Services.IServices;

namespace ECommerce.Application.Features.Emails.Commands.EmailCommandsHandlers;
public class ConfirmEmailCommandHandler :
    IRequestHandler<ConfirmEmailCommand, Response<ConfirmEmailResponseModel>>
{
    private readonly IUnitOfServices _services;
    public ConfirmEmailCommandHandler(
        IUnitOfServices services)
    {
        _services = services;
    }

    #region Confirm Email Handler
    public async Task<Response<ConfirmEmailResponseModel>>
        Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var model = await _services.EmailServices.ConfirmEmailAsync(request.Model.UserId, request.Model.Token);

        if (!model.IsEmailConfirmed)
            return ResponseHandler.Conflict(model);

        return ResponseHandler.Success(model);
    }
    #endregion
}
