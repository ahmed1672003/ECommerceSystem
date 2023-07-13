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
        RuleFor(c => c.CategoryViewModel.Name)
            .NotEmpty().WithMessage(c => $"{nameof(c.CategoryViewModel.Name)} can not be empty")
            .NotNull().WithMessage(c => $"{nameof(c.CategoryViewModel.Name)} can not be null")
            .MaximumLength(100).WithMessage(c => $"{nameof(c.CategoryViewModel.Name)} his length can not be bigger than {100}")
            .MinimumLength(1).WithMessage(c => $"{nameof(c.CategoryViewModel.Name)} his length can not be less than {1}");
    }
    private void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.CategoryViewModel)
            .MustAsync(async (CategoryViewModel, cancellationToken) =>
            !await _context.Categories.CanCreatedAsync(CategoryViewModel.Name, cancellationToken))
            .WithMessage(c => $"{c.CategoryViewModel.Name} is already exist !");
    }
}

