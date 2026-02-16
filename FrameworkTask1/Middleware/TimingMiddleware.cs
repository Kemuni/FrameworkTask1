using System.Diagnostics;
using FrameworkTask1.Services;

namespace FrameworkTask1.Middleware;

public class TimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TimingMiddleware> _logger;

    public TimingMiddleware(RequestDelegate next, ILogger<TimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        _logger.LogInformation(
            "[{RequestId}] метод={Method} путь={Path} статус={Status} Время выполнения: {ElapsedMs} мс",
            RequestId.GetOrCreate(context),
            context.Request.Method,
            context.Request.Path.Value ?? string.Empty,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds
        );
    }
}
