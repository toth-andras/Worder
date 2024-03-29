// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using Application.Auth;
using Application.Users.Services;
using Domain.Auth;
using Domain.Users;

namespace Infrastructure.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenRepository _tokenRepository;

    public AuthService(IUserService userService, ITokenRepository tokenRepository)
    {
        _userService = userService;
        _tokenRepository = tokenRepository;
    }

    public async Task<(User, Token)> Register(string email, string password)
    {
        var user = await _userService.CreateUser(email, password);
        var token = new Token(email, password);
        
        _tokenRepository.SaveToken(user.Id, token);

        return (user, token);
    }

    public async Task<(User, Token)> Login(string email, string password)
    {
        var user = await _userService.Login(email, password);
        var token = new Token(email, password);
        
        _tokenRepository.SaveToken(user.Id, token);

        return (user, token);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _userService.GetUserByEmail(email);
    }

    public async Task<User> ChangeEmail(string oldEmail, string newEmail)
    {
        return await _userService.UpdateEmail(oldEmail, newEmail);
    }

    public async Task<User> ChangePassword(string email, string oldPassword, string newPassword)
    {
        return await _userService.UpdatePassword(email, oldPassword, newPassword);
    }

    public async Task DeleteUser(string email)
    {
        await _userService.Delete(email);
    }

    public Task<Token?> RefreshToken(int userId, string refreshToken)
    {
        var tokenInStorage = _tokenRepository.GetToken(userId, TokenType.Refresh);
        if (tokenInStorage is null || tokenInStorage != refreshToken)
        {
            return null;
        }

        var newStr = userId + refreshToken + DateTime.UtcNow;
        var newToken = new Token(newStr, newStr + "refresh");
        
        _tokenRepository.SaveToken(userId, newToken);

        return Task.FromResult(newToken);
    }

    public Task<bool> VerifyToken(int userId, string token)
    {
        var tokenStored = _tokenRepository.GetToken(userId, TokenType.Main);

        return Task.FromResult(tokenStored is not null && token == tokenStored);
    }
}