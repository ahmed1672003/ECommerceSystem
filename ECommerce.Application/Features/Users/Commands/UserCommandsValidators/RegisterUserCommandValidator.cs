using ECommerce.Application.Features.Users.Commands.UserCommands;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsValidators;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IUnitOfWork _context;
    public RegisterUserCommandValidator(IUnitOfWork context)
    {
        _context = context;
        ApplyValidatorRules();
        ApplyCustomValidatorRules();
    }

    void ApplyValidatorRules()
    {
        RuleFor(c => c.model.Email)
            .NotNull().WithMessage(e => $"{nameof(e.model.Email)} is required !")
            .NotEmpty().WithMessage(e => $"{nameof(e.model.Email)} can not be null !")
            .EmailAddress().WithMessage(e => $"{nameof(e.model.Email)} not valid !");

        RuleFor(c => c.model.UserName)
           .NotNull().WithMessage(e => $"{nameof(e.model.Email)} is required !")
           .NotEmpty().WithMessage(e => $"{nameof(e.model.Email)} can not be null !");


        RuleFor(c => c.model)
            .Must((model) =>
            model.Password.Equals(model.ConfirmedPassword))
            .WithMessage("password & confirmedPassword are not equals !");
    }

    void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.model)
            .MustAsync(async (model, cancellationToken) =>
            !
           ((await _context.Users.IsExist(u => u.Email.Equals(model.Email))
            ||
            (await _context.Users.IsExist(u => u.UserName.Equals(model.UserName)))
           ))).WithMessage("email or user name is exist!");
    }
}
