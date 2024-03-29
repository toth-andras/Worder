// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using Application.Auth;
using Domain.Auth;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Infrastructure.Auth;

public class TokenRepositoryRedis : ITokenRepository
{
    private readonly string _connectionString;

    public TokenRepositoryRedis(IOptions<RedisOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }
    
    public void SaveToken(int userId, Token token)
    {
        var conn = ConnectionMultiplexer.Connect(_connectionString);
        var redisDB = conn.GetDatabase();

        redisDB.StringSet(GetKey(userId, TokenType.Main), token.TokenValue, TimeSpan.FromSeconds(token.TokenExpiresAfterSeconds));
        redisDB.StringSet(GetKey(userId, TokenType.Refresh), token.RefreshToken, TimeSpan.FromSeconds(token.RefreshTokenExpiresAfterSeconds));
    }

    public string? GetToken(int userId, TokenType type)
    {
        var redis = ConnectionMultiplexer.Connect(_connectionString).GetDatabase();
        var value = redis.StringGet(GetKey(userId, type));
        
        return  value.IsNullOrEmpty ? null : value.ToString();
    }

    private string GetKey(int userId, TokenType type)
    {
        return type switch
        {
            TokenType.Main => $"tokens:{userId}",
            TokenType.Refresh => $"refresh_tokens:{userId}",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}