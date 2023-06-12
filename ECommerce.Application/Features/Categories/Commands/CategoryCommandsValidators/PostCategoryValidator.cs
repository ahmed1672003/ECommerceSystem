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
            .NotEmpty().WithMessage(c => $"{nameof(c.CategoryDTO.Name)} can not be empty")
            .NotNull().WithMessage(c => $"{nameof(c.CategoryDTO.Name)} can not be null")
            .MaximumLength(100).WithMessage(c => $"{nameof(c.CategoryDTO.Name)} his length can not be bigger than {100}")
            .MinimumLength(1).WithMessage(c => $"{nameof(c.CategoryDTO.Name)} his length can not be less than {1}");
    }
    private void ApplyCustomValidatorRules()
    {
        RuleFor(c => c.CategoryDTO)
            .MustAsync(async (categoryDTO, cancellationToken) =>
            !await _context.Categories.CanCreated(categoryDTO.Name, cancellationToken))
            .WithMessage(c => $"{c.CategoryDTO.Name} is already exist !");
    }
}

