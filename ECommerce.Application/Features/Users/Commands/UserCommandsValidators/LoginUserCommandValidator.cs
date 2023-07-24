using ECommerce.Application.Features.Users.Commands.UserCommands;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsValidators;
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        ApplyValidatorRules();
    }

    void ApplyValidatorRules()
    {
        RuleFor(c => c.Model.EmailOrUserName)
            .NotNull().WithMessage(e => $"{nameof(e.Model.EmailOrUserName)} is required !")
            .NotEmpty().WithMessage(e => $"{nameof(e.Model.EmailOrUserName)} can not be null !");
    }
}
