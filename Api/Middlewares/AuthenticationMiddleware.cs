// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using Api.Controllers;
using Application.Auth;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Api.Middlewares;

public class AuthenticationMiddleware : IMiddleware
{
    private readonly IAuthService _authService;
    private readonly HashSet<string> _noAuthPaths;

    public AuthenticationMiddleware(IAuthService service)
    {
        _authService = service;

        _noAuthPaths = [
            "/auth/register",
            "/auth/login",
            "/tokens/refresh"
        ];
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (_noAuthPaths.Contains(context.Request.Path))
        {
            await next(context);
            return;
        }

        string userId = context.Request.Headers["userId"].FirstOrDefault() ?? throw new NotAuthorizedException("The user id was not provided");
        string token = context.Request.Headers["token"].FirstOrDefault() ??
                       throw new NotAuthorizedException("The token was not provided");

        var verification = await _authService.VerifyToken(int.Parse(userId), token);
        if (verification is false)
        {
            throw new NotAuthorizedException($"Incorrect token for user {userId}");
        }

        await next(context);
    }
}