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
        RuleFor(c => c.CategoryModel.Name)
            .NotEmpty().WithMessage(c => $"{nameof(c.CategoryModel.Name)} can not be empty")
            .NotNull().WithMessage(c => $"{nameof(c.CategoryModel.Name)} can not be null")
            .MaximumLength(100).WithMessage(c => $"{nameof(c.CategoryModel.Name)} his length can not be bigger than {100}")
            .MinimumLength(1).WithMessage(c => $"{nameof(c.CategoryModel.Name)} his length can not be less than {1}");
    }
    private void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.CategoryModel)
            .MustAsync(async (CategoryModel, cancellationToken) =>
            !await _context.Categories.CanCreatedAsync(CategoryModel.Name, cancellationToken))
            .WithMessage(c => $"{c.CategoryModel.Name} is already exist !");
    }
}

