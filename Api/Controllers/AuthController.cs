// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using ApiRequestModels;
using Application.Auth;
using Domain.Auth;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;
    private ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }
    
    [HttpPost("register")]
    public async Task<(User, Token)> Register([FromBody] EmailPasswordRequest request)
    {
       var result =  await _authService.Register(request.Email, request.Password);
       _logger.Log(LogLevel.Trace, $"Register: user {request.Email} on {DateTime.UtcNow}");
       
       return result;
    }

    [HttpPost("login")]
    public async Task<(User, Token)> Login([FromBody] EmailPasswordRequest request)
    {
       var result = await _authService.Login(request.Email, request.Password);
       _logger.Log(LogLevel.Trace, $"Login: user {request.Email} on {DateTime.UtcNow}");

       return result;
    }

    [HttpPut("change/email")]
    public async Task<User> ChangeEmail([FromBody] ChangeEmailRequest request)
    {
        var result = await _authService.ChangeEmail(request.OldEmail, request.NewEmail);
        _logger.Log(LogLevel.Trace, $"Email change: from {request.OldEmail} to {request.NewEmail}");

        return result;
    }

    [HttpPut("change/password")]
    public async Task<User> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var result = await _authService.ChangePassword(request.Email, request.OldPassword, request.NewPassword);
        _logger.Log(LogLevel.Trace, $"Password change: user {request.Email}");

        return result;
    }

    [HttpDelete("delete")]
    public async Task DeleteUser([FromBody] EmailRequest request)
    {
        await _authService.DeleteUser(request.Email);
        _logger.Log(LogLevel.Trace, $"Delete user: {request.Email}");
    }

    [HttpPost("tokens/verify")]
    public async Task<bool> VerifyToken([FromBody] VerifyTokenRequest request)
    {
        var result = await _authService.VerifyToken(request.UserId, request.Token);
        _logger.Log(LogLevel.Trace, $"Token verification for user with id {request.UserId}: {result}");
        
        return result;
    }

    [HttpPost("tokens/refresh")]
    public async Task<Token?> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshToken(request.UserId, request.RefreshToken);
        _logger.Log(LogLevel.Trace, $"Token refresh for user with id {request.UserId}: {result is not null}");
        
        return result;
    }
}