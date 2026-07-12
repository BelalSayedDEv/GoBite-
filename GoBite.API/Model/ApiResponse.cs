using Ardalis.Result;

namespace GoBite.API.Model;

public class ApiResponse<T>
{
    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }

    public bool IsSuccess { get; set; }

    public int StatusCode { get; set; }

    public List<string>? Errors { get; set; }

    public static ApiResponse<T> Success(T? data, string? message = null)
    {
        return new ApiResponse<T>
        {
            IsSuccess = true,
            Data = data,
            Message = message ?? string.Empty
        };
    }

    public static ApiResponse<T> Failure(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
    }
}

public static class ApiResponse
{
    public static ApiResponse<T> ToApiResponse<T>(this Result<T> result)
    {
        return new ApiResponse<T>
        {
            IsSuccess = result.IsSuccess,
            Message = string.Join("; ", result.Errors),
            Data = result.Value,
            StatusCode = MapStatus(result.Status)
        };
    }

    public static ApiResponse<object> ToApiResponse(this Result result)
    {
        return new ApiResponse<object>
        {
            IsSuccess = result.IsSuccess,
            Message = string.Join("; ", result.Errors),
            Data = null,
            StatusCode = MapStatus(result.Status)
        };
    }

    private static int MapStatus(ResultStatus status)
    {
        return status switch
        {
            ResultStatus.NotFound => StatusCodes.Status404NotFound,
            ResultStatus.Unauthorized => StatusCodes.Status401Unauthorized,
            ResultStatus.Forbidden => StatusCodes.Status403Forbidden,
            ResultStatus.Conflict => StatusCodes.Status409Conflict,
            ResultStatus.Invalid => StatusCodes.Status400BadRequest,
            ResultStatus.Error => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status200OK
        };
    }
}
