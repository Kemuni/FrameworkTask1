using System.Text.Json;
using FrameworkTask1.Errors;
using FrameworkTask1.Models;
using FrameworkTask1.Services;

namespace FrameworkTask1.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestId = RequestId.GetOrCreate(context);

        try
        {
            await _next(context);
        }
        catch (BaseException ex)
        {
            _logger.LogWarning(ex, "Обработанная ошибка \"{ExceptionMessage}\". requestId={RequestId}", ex.Code, requestId);
            await WriteError(context, ex.StatusCode, ex.Code, ex.Message, requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Непредвиденная ошибка. requestId={RequestId}", requestId);
            await WriteError(context, 500, "internal_error", "Внутренняя ошибка сервера", requestId);
        }
    }
    
    
    private static async Task WriteError(HttpContext context, int statusCode, string code, string message, string requestId)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json; charset=utf-8";

        var payload = new ErrorResponse
        {
            ErrorCode = code,
            Message = message,
            RequestId = requestId
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}
