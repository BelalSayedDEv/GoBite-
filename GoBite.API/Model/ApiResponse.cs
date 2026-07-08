namespace GoBite.API.Model;

public class ApiResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
    public bool IsSuccess { get; set; }

    public static ApiResponse<T> Success(T? data, string? message = null)
    {
        return new ApiResponse<T>
        {
            Message = message ?? string.Empty,
            Data = data,
            IsSuccess = true
        };
    }

    public static ApiResponse<T> Failure(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Message = message,
            Data = default,
            IsSuccess = false,
            Errors = errors
        };
    }
}
