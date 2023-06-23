using System.ComponentModel.DataAnnotations;

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
        RuleFor(c => c.DTO.Email)
            .NotNull().WithMessage(p => $"{nameof(p.DTO.Email)} is required !")
            .NotEmpty().WithMessage(p => $"{nameof(p.DTO.Email)} can not be empty !")
            .When(c =>
            new EmailAddressAttribute().IsValid(c.DTO.Email))
            .WithMessage(p => $"{nameof(p.DTO.Email)} is not valid");

        RuleFor(c => c)
        .Must(p => p.DTO.Password.Equals(p.DTO.ConfirmedPassword))
        .WithMessage(p => $"{nameof(p.DTO.Password)} and {nameof(p.DTO.ConfirmedPassword)} are not equal !");
    }
    void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.DTO)
            .MustAsync(async (dto, cancellationToken) =>
           !await _context.Users.IsExist(u =>
            u.Email!.Equals(dto.Email) || u.UserName!.Equals(dto.UserName), cancellationToken))
            .WithMessage(c => $"{nameof(c.DTO.Email)} or {nameof(c.DTO.UserName)} are exist !");
    }
}
