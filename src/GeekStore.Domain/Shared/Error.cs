namespace GeekStore.Domain.Shared
{
    public class Error
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }
        public Error(string code, IEnumerable<string> errorMessages)
        {
            Code = code;
            ErrorMessages = errorMessages;
        }
        public string Code { get; }
        public string? Message { get; }
        public IEnumerable<string>? ErrorMessages { get; }
    }
}