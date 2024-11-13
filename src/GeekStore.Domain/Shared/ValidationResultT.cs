using GeekStore.Domain.Interfaces;

namespace GeekStore.Domain.Shared;
public sealed class ValidationResult<T> : Result<T>
{
    private ValidationResult(Error[] errors)
        : base(IValidationResult.ValidationError, errors) { }

    public static ValidationResult<T> WithErrors(Error[] errors) => new(errors);
}
