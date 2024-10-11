using GeekStore.Domain.Interfaces;

namespace GeekStore.Domain.Shared
{
    public class Result<T> : IResult
    {
        private Result(T value)
        {
            Value = value;
            Error = null;
        }
        private Result(Error error)
        {
            Error = error;
            Value = default;
        }
        public T? Value{ get; }
        public Error? Error{ get; }
        public bool IsSuccess => Error is null;
        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(Error error) => new Result<T>(error);

        public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
        }
    }
}