﻿namespace ECommerce.Application.Features.Categories.Commands.CategoryCommandsValidators;
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
         .NotEmpty().WithMessage(
            c => $"{nameof(c.Id)} can not be not empty")
         .NotNull().WithMessage(
            c => $"{nameof(c.Id)} can not be not null")
         .MaximumLength(36).WithMessage(
            c => $"{nameof(c.Id)} his length can not be bigger than {36}")
         .MinimumLength(36).WithMessage(
            c => $"{nameof(c.Id)} his length can not be less than {36}");

        RuleFor(c => c.CategoryModel.Name)
        .NotEmpty().WithMessage(
            c => $"{nameof(c.CategoryModel.Name)} can not be not empty")
        .NotNull().WithMessage(
            c => $"{nameof(c.CategoryModel.Name)} can not be not null")
        .MaximumLength(100).WithMessage(
            c => $"{nameof(c.CategoryModel.Name)} his length can not be bigger than {100}")
        .MinimumLength(1).WithMessage(
            c => $"{nameof(c.CategoryModel.Name)} his length can not be less than {1}");
    }
    private void ApplyCustomValidatorRules()
    {
        RuleFor(c => c)
        .MustAsync(async (command, cancellationToken) =>
        await _context.Categories.IsExistAsync(
            c => c.Id.Equals(command.Id) && c.Id.Equals(command.CategoryModel.Id)))
        .WithMessage(
            c => $"{nameof(c.Id)} is not exist !");

        RuleFor(c => c)
            .MustAsync(async (command, cancellationToken) =>
           await _context.Categories.CanUpdatedAsync(command.CategoryModel.Name, command.Id))
            .WithMessage(
            c => $"{nameof(c.CategoryModel.Name)} is Exist !");
    }
}
