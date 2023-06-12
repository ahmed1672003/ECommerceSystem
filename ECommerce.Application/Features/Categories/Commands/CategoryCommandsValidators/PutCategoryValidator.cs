namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsValidators;
public class PutCategoryValidator : AbstractValidator<PutCategoryCommand>
{
    private readonly IUnitOfWork _context;

    public PutCategoryValidator(IUnitOfWork context)
    {
        _context = context;
        ApplyValidatorRules();
        ApplyCustomValidatorRules();
    }

    private void ApplyValidatorRules()
    {
        RuleFor(c => c.Id)
         .NotEmpty().WithMessage("Name can not be not empty")
         .NotNull().WithMessage("Name can not be not null")
         .MaximumLength(36).WithMessage("{PropertyName} his length can not be bigger than 36")
         .MinimumLength(36).WithMessage("{PropertyName} his length can not be less than 36");

        RuleFor(c => c.CategoryDTO.Name)
        .NotEmpty().WithMessage("Name can not be not empty")
        .NotNull().WithMessage("Name can not be not null")
        .MaximumLength(100).WithMessage("Name his length can not be bigger than 100")
        .MinimumLength(1).WithMessage("Name his length can not be less than 1");
    }

    private void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.CategoryDTO.Name)
            .MustAsync(async (Key, CancellationToken) =>
            await _context.Categories.IsExist(c => !c.Name.Equals(Key)))
            .WithMessage("Category name is exist !");

        RuleFor(c => c.Id)
            .MustAsync(async (Key, CancellationToken) =>
            await _context.Categories.IsExist(c => c.Id.Equals(Key)))
            .WithMessage("Category Id is not exist !");
    }
}
