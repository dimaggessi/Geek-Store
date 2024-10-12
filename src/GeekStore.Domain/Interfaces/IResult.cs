using GeekStore.Domain.Shared;

namespace GeekStore.Domain.Interfaces;
public interface IResult
{
    Error? Error { get; }
    bool IsSuccess { get; }
    bool IsFailure { get; }
}
