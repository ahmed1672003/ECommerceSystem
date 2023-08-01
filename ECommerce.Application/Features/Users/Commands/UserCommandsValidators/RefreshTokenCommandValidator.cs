using ECommerce.Application.Features.Users.Commands.UserCommands;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsValidators;
public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        ApplyValidatorRules();
    }

    void ApplyValidatorRules()
    {
        RuleFor(c => c.RefreshTokenRequestModel.RefreshJWT)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.RefreshTokenRequestModel.JWT)
          .NotNull()
          .NotEmpty();
    }
}
