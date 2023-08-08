using ECommerce.Application.Features.Emails.Commands.EmailCommands;

namespace ECommerce.Application.Features.Emails.Commands.EmailCommandsValidators;
public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        ApplyValidatorRules();
    }
    void ApplyValidatorRules()
    {
        RuleFor(c => c.Model.Code)
        .NotNull()
        .NotEmpty();
        RuleFor(c => c.Model.UserId)
        .NotNull()
        .NotEmpty()
        .Length(36);
    }
}
