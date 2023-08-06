//using ECommerce.Application.Features.Users.Commands.UserCommands;

//namespace ECommerce.Application.Features.Users.Commands.UserCommandsValidators;
//public class AddUserToRoleCommandValidator : AbstractValidator<AddUserToRoleCommand>
//{

//    public AddUserToRoleCommandValidator()
//    {
//        ApplyValidatorRules();
//    }

//    void ApplyValidatorRules()
//    {

//        RuleFor(c => c.Model.UserId)
//            .NotNull()
//            .NotEmpty()
//            .Length(36, 36);

//        RuleFor(c => c.Model.RoleName)
//            .NotNull()
//            .NotEmpty();
//    }
//}
