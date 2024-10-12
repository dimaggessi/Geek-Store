using GeekStore.Domain.Interfaces;

namespace GeekStore.Domain.Shared;
public class Result : IResult
{
    protected internal Result()
    {
        Error = null;
        ValidationErrors = null;
    }
    protected internal Result(Error error)
    {
        Error = error;
    }
    protected internal Result(Error error, Error[] errors)
    {
        Error = error;
        ValidationErrors = errors;
    }
    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }
    public Error[]? ValidationErrors { get; }

    public static Result Failure(Error error) => new(error);
    public static Result<T> Failure<T>(Error error) => new Result<T>(error);
    public static Result Success() => new();
    public static Result<T> Success<T>(T value) => new Result<T>(value);
}
