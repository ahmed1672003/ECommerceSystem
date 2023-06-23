using ECommerce.Application.Features.Users.Queries.UserQueries;

namespace ECommerce.Application.Features.Users.Queries.UserQueriesValidators;
public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    private readonly IUnitOfWork _context;
    public GetUserByIdQueryValidator(IUnitOfWork context)
    {
        _context = context;
        ApplyValidatorRules();
        ApplyCustomValidatorRules();
    }

    void ApplyValidatorRules()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage(c => $"{nameof(c.Id)} is required field !")
            .NotNull().WithMessage(c => $"{nameof(c.Id)} is can not be null !")
            .Must(c => c.Length == 36).WithMessage(c => $"{nameof(c.Id)} must be 36 chars !");
    }
    void ApplyCustomValidatorRules()
    {

    }
}
