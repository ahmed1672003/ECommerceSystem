namespace ECommerce.Application.Features.Categories.Queries.CategoryQueriesValidators;
public class GetCategoryByIdValidator : AbstractValidator<GetCategoryByIdQuery>
{
    private readonly IUnitOfWork _context;

    public GetCategoryByIdValidator(IUnitOfWork context)
    {
        _context = context;
        ApplyValidatorRules();
        ApplyCustomValidatorRules();
    }
    private void ApplyValidatorRules()
    {
        RuleFor(c => c.Id)
        .NotEmpty().WithMessage("Id can not be not empty")
        .NotNull().WithMessage("Id can not be not null")
        .MaximumLength(36).WithMessage("{PropertyName} his length can not be bigger than 36")
        .MinimumLength(36).WithMessage("{PropertyName} his length can not be less than 36");
    }
    private void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.Id)
            .MustAsync(async (id, cancellationToken) =>
            await _context.Categories.IsExist(c => c.Id.Equals(id), cancellationToken))
            .WithMessage("Id is not found !");
    }
}
