using Serilog;
using System.Diagnostics;

namespace Para.Api.Middleware;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;

    public LoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

            await _next(context);

        stopwatch.Stop();
        var logMessage = $"HTTP {context.Request.Method} - {context.Request.Path} responded {context.Response.StatusCode} in {stopwatch.ElapsedMilliseconds} ms";
        Log.Information(logMessage);
    }
}