// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using Domain.Auth;

namespace Application.Auth;

public interface ITokenRepository
{
    public void SaveToken(int userId, Token token);

    public string? GetToken(int userId, TokenType type);
}