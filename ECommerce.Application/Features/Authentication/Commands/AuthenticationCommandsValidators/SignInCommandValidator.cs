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
        RuleFor(c => c.DTO)
            .NotNull().WithMessage(c => $"{nameof(c.DTO)} can not be null")
            .NotEmpty().WithMessage(c => $"{nameof(c.DTO)} can not be empty");

        RuleFor(c => c.DTO.EmailOrUserName)
            .NotNull().WithMessage(c => $"{nameof(c.DTO.EmailOrUserName)} can not be null")
            .NotEmpty().WithMessage(c => $"{nameof(c.DTO.EmailOrUserName)} can not be empty");

        RuleFor(c => c.DTO.Password)
            .NotNull().WithMessage(c => $"{nameof(c.DTO.Password)} can not be null")
            .NotEmpty().WithMessage(c => $"{nameof(c.DTO.Password)} can not be empty");
    }

    void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.DTO.EmailOrUserName)
            .MustAsync(async (emailOrUserName, cancellationToken) =>
            await _context.Users.IsExist(
                c => new EmailAddressAttribute().IsValid(emailOrUserName) ?
                c.Email.Equals(emailOrUserName) :
                c.UserName.Equals(emailOrUserName), cancellationToken)
          ).WithMessage(c => $"{nameof(c.DTO.EmailOrUserName)} is not exist !");
    }
}
