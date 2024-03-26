// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 24.3.2024

using Application.Users.Repositories;
using Dapper;
using Domain.Users;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using Migrations.Descriptors;
using Migrations.TableDescriptors;
using Npgsql;

namespace Infrastructure.Users.Repositories;

public class UserRepositoryPostgre : IUserRepository
{
    private readonly string _connectionString;

    public UserRepositoryPostgre(IOptions<PostgresOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public async Task<User> CreateUser(string email, string password)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        string salt = await connection.QuerySingleAsync<string>("SELECT gen_salt('bf');");

        var query =
            $@"
                INSERT 
                INTO {SchemaDescriptor.SchemaName}.{UserTable.TableName} ({UserTable.Email}, {UserTable.Password}, {UserTable.Salt})
                VALUES (@Email, crypt(@Password, @Salt), @Salt) 
                RETURNING 
                    {UserTable.Id} AS {nameof(User.Id)},
                    {UserTable.Email} AS {nameof(User.Email)},
                    {UserTable.Password} AS {nameof(User.PasswordHash)}
            ";

        return await connection.QuerySingleAsync<User>(query, new {Email = email, Password = password, Salt = salt});
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var query =
            $@"
                SELECT 
                    {UserTable.Id} AS {nameof(User.Id)},
                    {UserTable.Email} AS {nameof(User.Email)},
                    {UserTable.Password} AS {nameof(User.PasswordHash)}
                FROM {SchemaDescriptor.SchemaName}.{UserTable.TableName}
                WHERE {UserTable.Email}=@Email
            ";

        return await connection.QuerySingleOrDefaultAsync<User>(query, new {Email = email});
    }

    public async Task<bool> CheckPassword(string email, string password)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var query =
            $@"
                SELECT 
                    {UserTable.Salt} AS Salt,
                    {UserTable.Password} AS Password
                FROM {SchemaDescriptor.SchemaName}.{UserTable.TableName}
                WHERE {UserTable.Email}=@Email
            ";

        (string Salt, string Password)? saltPassword =
            await connection.QuerySingleOrDefaultAsync<(string Salt, string Password)>(query, new {Email = email});

        if (saltPassword is null)
        {
            return false;
        }

        string hashToCheck = await connection.QuerySingleAsync<string>("SELECT crypt(@Password, @Salt)",
            new {Password = password, Salt = saltPassword.Value.Salt});

        return hashToCheck == saltPassword.Value.Password;
    }

    public async Task<User?> UpdateEmail(string oldEmail, string newEmail)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var query =
            $@"
                UPDATE {SchemaDescriptor.SchemaName}.{UserTable.TableName} 
                SET {UserTable.Email} = @EmailNew
                WHERE {UserTable.Email}=@EmailOld
                RETURNING 
                    {UserTable.Id} AS {nameof(User.Id)},
                    {UserTable.Email} AS {nameof(User.Email)},
                    {UserTable.Password} AS {nameof(User.PasswordHash)}
            ";

        return await connection.QuerySingleOrDefaultAsync<User>(query, new {EmailOld = oldEmail, EmailNew = newEmail});
    }

    public async Task<User?> UpdatePassword(string email, string newPassword)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var queryGetSalt =
            $@"
                SELECT 
                    {UserTable.Salt}
                FROM {SchemaDescriptor.SchemaName}.{UserTable.TableName}
                WHERE {UserTable.Email}=@Email
            ";
        var salt = await connection.QuerySingleOrDefaultAsync<string>(queryGetSalt, new {Email = email});
        if (salt is null)
        {
            throw new Exception("No salt for user");
        }

        var query =
            $@"
                UPDATE {SchemaDescriptor.SchemaName}.{UserTable.TableName} 
                SET {UserTable.Password} = crypt(@Password, @Salt)
                WHERE {UserTable.Email}=@Email
                RETURNING 
                    {UserTable.Id} AS {nameof(User.Id)},
                    {UserTable.Email} AS {nameof(User.Email)},
                    {UserTable.Password} AS {nameof(User.PasswordHash)}
            ";

        return await connection.QuerySingleOrDefaultAsync<User>(query,
            new {Email = email, Password = newPassword, Salt = salt});
    }

    public async Task Delete(string email)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var query =
            $@"
                DELETE
                FROM {SchemaDescriptor.SchemaName}.{UserTable.TableName}
                WHERE {UserTable.Email}=@Email
            ";

        await connection.ExecuteAsync(query, new {Email = email});
    }
}