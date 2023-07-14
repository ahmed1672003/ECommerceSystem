namespace ECommerce.Application.Features.Authentication.Commands.AuthenticationCommandsValidators;
public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {

    }
    private void ApplyValidatorRules()
    {
        RuleFor(x => x.RefreshToken)
            .NotNull();

        RuleFor(x => x.AccessToken)
            .NotNull();
    }
}
