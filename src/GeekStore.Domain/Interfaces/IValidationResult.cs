using GeekStore.Domain.Shared;

namespace GeekStore.Domain.Interfaces
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
            "ValidationError", "A validation problem occurred.");
    }
}
