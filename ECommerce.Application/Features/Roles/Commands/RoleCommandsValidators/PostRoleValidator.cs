using ECommerce.Application.Features.Roles.Commands.RoleCommands;

namespace ECommerce.Application.Features.Roles.Commands.RoleCommandsValidators;
public class PostRoleValidator : AbstractValidator<PostRoleCommand>
{
    public PostRoleValidator()
    {
        ApplyValidatorRules();
    }
    void ApplyValidatorRules()
    {
        RuleFor(e => e.Model.Name)
            .NotNull()
            .NotEmpty();
    }


    void ApplyCustomValidatorRules()
    {

    }
}
