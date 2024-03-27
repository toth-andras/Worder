// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

using System.Net;
using System.Text.Json;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private IWebHostEnvironment _environment;
    private ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(IWebHostEnvironment environment, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _environment = environment;
        _logger = logger;
    }
    
    private async Task SetResponseOnError(HttpResponse response, int responseCode, Exception? exception = null, string? message = null,
        bool includeDetails = false)
    {
        response.StatusCode = responseCode;
        ProblemDetails details = new() {Title = message, Detail = $"{exception?.GetType().Name}{(includeDetails ? $": {exception?.Message}" : null)}"};
        response.ContentType = "application/json";
        
        await response.WriteAsync(JsonSerializer.Serialize(details));
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            await SetResponseOnError(context.Response, (int) HttpStatusCode.NotFound, e, "Invalid request", true);
        }
        catch (IncorrectPasswordException e)
        {
            await SetResponseOnError(context.Response, (int) HttpStatusCode.Forbidden, e, "Incorrect password", true);
        }
        catch (EmailAlreadyInUseException e)
        {
            await SetResponseOnError(context.Response, (int) HttpStatusCode.BadRequest, e, "Email duplicate", true);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, $"Internal server error in {context.Request.HttpContext.TraceIdentifier} request");
            await SetResponseOnError(context.Response, (int) HttpStatusCode.InternalServerError, e, "Internal error",
                _environment.IsDevelopment());
        }
    }
}