namespace ECommerce.Application.Features.Authentication.Commands.AuthenticationCommandsValidators;
public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    private readonly IUnitOfWork _context;

    public SignInCommandValidator(IUnitOfWork context)
    {
        _context = context;
        ApplyValidatorRules();
        ApplyCustomValidatorRules();
    }

    void ApplyValidatorRules()
    {
        RuleFor(c => c.model)
            .NotNull().WithMessage(c => $"{nameof(c.model)} can not be null")
            .NotEmpty().WithMessage(c => $"{nameof(c.model)} can not be empty");

        RuleFor(c => c.model.EmailOrUserName)
            .NotNull().WithMessage(c => $"{nameof(c.model.EmailOrUserName)} can not be null")
            .NotEmpty().WithMessage(c => $"{nameof(c.model.EmailOrUserName)} can not be empty");

        RuleFor(c => c.model.Password)
            .NotNull().WithMessage(c => $"{nameof(c.model.Password)} can not be null")
            .NotEmpty().WithMessage(c => $"{nameof(c.model.Password)} can not be empty");
    }

    void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.model.EmailOrUserName)
            .MustAsync(async (emailOrUserName, cancellationToken) =>
            await _context.Users.IsExist(
                c => new EmailAddressAttribute().IsValid(emailOrUserName) ?
                c.Email.Equals(emailOrUserName) :
                c.UserName.Equals(emailOrUserName), cancellationToken)
          ).WithMessage(c => $"{nameof(c.model.EmailOrUserName)} is not exist !");
    }
}
