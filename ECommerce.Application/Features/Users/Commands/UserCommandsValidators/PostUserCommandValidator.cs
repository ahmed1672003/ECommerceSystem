using ECommerce.Application.Features.Users.Commands.UserCommands;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsValidators;
public class PostUserCommandValidator : AbstractValidator<PostUserCommand>
{
    private readonly IUnitOfWork _context;
    public PostUserCommandValidator(IUnitOfWork context)
    {
        _context = context;
        ApplyValidatorRules();
        ApplyCustomValidatorRules();
    }
    void ApplyValidatorRules()
    {
        RuleFor(c => c.model.Email)
            .NotNull().WithMessage(p => $"{nameof(p.model.Email)} is required !")
            .NotEmpty().WithMessage(p => $"{nameof(p.model.Email)} can not be empty !")
            .When(c =>
            new EmailAddressAttribute().IsValid(c.model.Email))
            .WithMessage(p => $"{nameof(p.model.Email)} is not valid");

        RuleFor(c => c)
        .Must(p => p.model.Password.Equals(p.model.ConfirmedPassword))
        .WithMessage(p => $"{nameof(p.model.Password)} and {nameof(p.model.ConfirmedPassword)} are not equal !");
    }
    void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.model)
            .MustAsync(async (model, cancellationToken) =>
           !await _context.Users.IsExist(u =>
            u.Email!.Equals(model.Email) || u.UserName!.Equals(model.UserName), cancellationToken))
            .WithMessage(c => $"{nameof(c.model.Email)} or {nameof(c.model.UserName)} are exist !");
    }
}
