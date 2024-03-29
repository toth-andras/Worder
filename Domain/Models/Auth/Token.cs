// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using System.Security.Cryptography;
using System.Text;

namespace Domain.Auth;

public class Token
{
    public Token(string email, string password, long tokenExpiresAfterSeconds = 28800, long refreshTokenExpiresAfterSeconds = 720000)
    {
        using var hasher = new SHA1Managed();

        TokenValue = GetStringRepresentation(hasher.ComputeHash(Encoding.UTF32.GetBytes(email + password + DateTime.UtcNow)));
        RefreshToken =
            GetStringRepresentation(hasher.ComputeHash(Encoding.UTF32.GetBytes(email + password + DateTime.UtcNow + "refresh")));

        TokenExpiresAfterSeconds = tokenExpiresAfterSeconds;
        RefreshTokenExpiresAfterSeconds = refreshTokenExpiresAfterSeconds;
    }
    
    public string TokenValue { get; private init; }
    public string RefreshToken { get; private init; }
    
    public long TokenExpiresAfterSeconds { get; private init; }
    
    public long RefreshTokenExpiresAfterSeconds { get; private init; }

    private string GetStringRepresentation(byte[] bytes)
    {
        StringBuilder builder = new();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }
        return builder.ToString();
    }
}

public enum TokenType
{
    Main,
    Refresh
}