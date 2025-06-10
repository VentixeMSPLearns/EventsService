
namespace Data.Models;

public class Result
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; } = null;
}

public class Result<T> : Result
{
    public T? Data { get; set; } = default;

    public static Result<T> SuccessResult(T data)
    {
        return new Result<T>
        {
            Success = true,
            Data = data
        };
    }
    public static Result<T> FailureResult(string errorMessage)
    {
        return new Result<T>
        {
            Success = false,
            ErrorMessage = errorMessage
        };
    }
}
