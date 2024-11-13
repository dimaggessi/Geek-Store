using GeekStore.Domain.Shared;

namespace GeekStore.Domain.Interfaces
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
            ResourceErrorMessages.VALIDATION_ERROR_CODE, 
            ResourceErrorMessages.VALIDATION_ERROR_MESSAGE);
    }
}
