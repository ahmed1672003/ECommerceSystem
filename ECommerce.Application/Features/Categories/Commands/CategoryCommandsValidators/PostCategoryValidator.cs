namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsValidators;
public class PostCategoryValidator : AbstractValidator<PostCategoryCommand>
{
    private readonly IUnitOfWork _context;
    public PostCategoryValidator(IUnitOfWork context)
    {
        _context = context;
        ApplyValidatorRules();
        ApplyCustomValidatorRules();
    }

    private void ApplyValidatorRules()
    {
        RuleFor(c => c.CategoryDTO.Name)
            .NotEmpty().WithMessage("Name can not be not empty")
            .NotNull().WithMessage("Name can not be not null")
            .MaximumLength(100).WithMessage("Name his length can not be bigger than 100")
            .MinimumLength(1).WithMessage("Name his length can not be less than 1");
    }

    private void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.CategoryDTO.Name)
            .MustAsync(async (Key, cancellationToken) =>
            await _context.Categories.IsExist(c => !c.Name.Equals(Key))
            ).WithMessage("Category name is exist !");
    }
}

