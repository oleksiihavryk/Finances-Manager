namespace OleksiiHavryk.Finances.API.Middleware;
/// <summary>
///     Middleware for exception handling.
/// </summary>
public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handled by Exception Handler Middleware.");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(
                text: "Unknown error has occurred on server side.");
        }
    }
}