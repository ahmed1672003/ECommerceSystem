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
        RuleFor(c => c.Model.Email)
            .NotNull().WithMessage(e => $"{nameof(e.Model.Email)} is required !")
            .NotEmpty().WithMessage(e => $"{nameof(e.Model.Email)} can not be null !")
            .EmailAddress().WithMessage(e => $"{nameof(e.Model.Email)} not valid !");

        RuleFor(c => c.Model.UserName)
           .NotNull().WithMessage(e => $"{nameof(e.Model.Email)} is required !")
           .NotEmpty().WithMessage(e => $"{nameof(e.Model.Email)} can not be null !");


        RuleFor(c => c.Model)
            .Must((Model) =>
            Model.Password.Equals(Model.ConfirmedPassword))
            .WithMessage("password & confirmedPassword are not equals !");
    }

    void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.Model)
            .MustAsync(async (Model, cancellationToken) =>
            !
           ((await _context.Users.IsExist(u => u.Email.Equals(Model.Email))
            ||
            (await _context.Users.IsExist(u => u.UserName.Equals(Model.UserName)))
           ))).WithMessage("email or user name is exist!");
    }
}
