using ECommerce.Application.Features.Authentication.Queries.AuthenticationQueries;

namespace ECommerce.Application.Features.Authentication.Queries.AuthenticationQueriesValidators;
public class GetUserRefreshTokenQueryValidator : AbstractValidator<GetUserRefreshTokenQuery>
{
    private readonly IAuthenticationServices _authenticationServices;
    private readonly IUnitOfWork _context;
    public GetUserRefreshTokenQueryValidator(IAuthenticationServices authenticationServices, IUnitOfWork context)
    {
        _authenticationServices = authenticationServices;
        ApplyValidatorRules();
        ApplyCustomValidatorRules();
        _context = context;
    }

    void ApplyValidatorRules()
    {
    }

    void ApplyCustomValidatorRules()
    {
        RuleFor(q => q.AccessToken)
            .MustAsync(async (accessToken, cancellationToken) =>
            await _authenticationServices.ValidateTokenAsync(accessToken))
            .WithMessage(accessToken => $"this token [{accessToken.AccessToken}] not valid !");

        RuleFor(q => q.AccessToken)
            .MustAsync(async (accessToken, cancellationToken) =>
            (await _context.UserRefreshTokens.RetrieveAsync(
                 urt => urt.AccessToken.Equals(accessToken))).IsAccessTokenActive)
            .WithMessage(accessToken => $"this token [{accessToken.AccessToken}] not active !");
    }
}
