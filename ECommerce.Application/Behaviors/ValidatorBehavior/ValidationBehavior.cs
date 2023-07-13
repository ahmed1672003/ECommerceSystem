using ValidationException = FluentValidation.ValidationException;

namespace ECommerce.Application.Behaviors.ValidatorBehavior;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    private IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse>
        Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                        );
            var failures = results
                                                    .SelectMany(vr => vr.Errors)
                                                    .Where(f => f != null);
            if (failures.Count() != 0)
            {
                var message = failures.Select(vf => vf.ErrorMessage).FirstOrDefault();
                throw new ValidationException(message, failures);
            }
        }
        return await next();
    }
}
