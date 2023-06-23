using ECommerce.Application.Features.Users.Queries.UserQueries;

namespace ECommerce.Application.Features.Users.Queries.UserQueriesValidators;
public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
{
    private readonly IUnitOfWork _context;
    public GetAllUsersQueryValidator(IUnitOfWork context)
    {
        _context = context;
        ApplyCustomValidatorRules();
    }

    void ApplyCustomValidatorRules()
    {

    }
}
