using System.ComponentModel.DataAnnotations;

using ECommerce.Application.Features.Emails.Commands.EmailCommands;

namespace ECommerce.Application.Features.Emails.Commands.EmailCommandsValidators;
partial class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        ApplyValidatorRules();
    }
    void ApplyValidatorRules()
    {
        RuleFor(c => c.Model.MailTo)
            .NotNull()
            .NotEmpty()
            .Must((email) => new EmailAddressAttribute().IsValid(email)).WithMessage("email is npt valid");

        RuleFor(c => c.Model.Body)
        .NotNull()
        .NotEmpty();

        RuleFor(c => c.Model.Subject)
        .NotNull()
        .NotEmpty();
    }
}
