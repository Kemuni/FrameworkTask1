using FrameworkTask1.Services;

namespace FrameworkTask1.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestId = RequestId.GetOrCreate(context);

        _logger.LogInformation(
            "[{RequestId}] Входящий запрос: {Method} {Path}",
            requestId,
            context.Request.Method,
            context.Request.Path
        );

        await _next(context);

        _logger.LogInformation(
            "[{RequestId}] Ответ: {StatusCode}",
            requestId,
            context.Response.StatusCode
        );
    }
}
