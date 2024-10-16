namespace GeekStore.Domain.Shared
{
    public class Result<T> : Result
    {
        protected internal Result(T value) : base()
        {
            Value = value;
        }
        protected internal Result(Error error) : base(error)
        {
            Value = default;
        }
        protected internal Result(Error error, Error[] errors) : base(error, errors)
        {
            Value = default;
        }
        public T? Value { get; }

        public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
        }
    }
}