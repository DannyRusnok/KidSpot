namespace KidSpot.Application.Common;

public class ApiResponse<T>
{
    public T? Data { get; init; }
    public ApiError? Error { get; init; }

    public static ApiResponse<T> Success(T data) => new() { Data = data, Error = null };
    public static ApiResponse<T> Failure(string code, string message) =>
        new() { Data = default, Error = new ApiError(code, message) };
}

public record ApiError(string Code, string Message);
