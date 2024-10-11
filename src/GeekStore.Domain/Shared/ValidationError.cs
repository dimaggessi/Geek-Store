namespace GeekStore.Domain.Shared
{
    public class ValidationError : Error
    {
        public ValidationError(IEnumerable<string> errorMessages) : base("Validation Error", errorMessages) {}
    }
}