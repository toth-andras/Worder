// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

namespace Api.Middlewares;

public class RequestResponseLogMiddleware : IMiddleware
{
    private readonly ILogger<RequestResponseLogMiddleware> _logger;

    public RequestResponseLogMiddleware(ILogger<RequestResponseLogMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string identifier = context.Request.HttpContext.TraceIdentifier;

        // Log request
        var basicRequestLog = $"Request with id {identifier} to {context.Request.Path} ";
        _logger.Log(LogLevel.Information, basicRequestLog);
        var advancedRequestLog = $"Query string: {context.Request.QueryString.Value} " +
                                 await context.Request.GetBodyAsStringAsync();
        _logger.Log(LogLevel.Trace, basicRequestLog + advancedRequestLog);
        
        await next(context);

        // Log response
        var basicResponseLog = $"Response for the request with id {identifier}. Status code: {context.Response.StatusCode}";
        _logger.Log(LogLevel.Information, basicResponseLog);
    }
}