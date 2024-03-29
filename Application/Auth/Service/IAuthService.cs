// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using Domain.Auth;
using Domain.Users;

namespace Application.Auth;

public interface IAuthService
{
    public Task<(User, Token)> Register(string email, string password);

    public Task<(User, Token)> Login(string email, string password);

    public Task<User> GetUserByEmail(string email);

    public Task<User> ChangeEmail(string oldEmail, string newEmail);

    public Task<User> ChangePassword(string email, string oldPassword, string newPassword);

    public Task DeleteUser(string email);

    public Task<Token?> RefreshToken(int userId, string refreshToken);

    public Task<bool> VerifyToken(int userId, string token);
}