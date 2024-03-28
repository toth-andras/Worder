// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using System.Data;
using Application.Exceptions;
using Application.Repositories;
using Dapper;
using Dapper.Transaction;
using Domain.Flashcards;
using Migrations.Descriptors;

namespace Infrastructure.Flashcards.Tags.Repositories;

public class TagRepositoryPostgreSql : ITagRepository
{
    public async Task<Tag> Create(int userId, string name, IDbConnection connection, IDbTransaction? transaction = null)
    {
        var query =
            $@"
                INSERT INTO {SchemaDescriptor.SchemaName}.{TagTable.TableName} ({TagTable.UserId}, {TagTable.TagName})
                VALUES (@UserId, @TagName)
                RETURNING
                    {TagTable.Id} AS {nameof(Tag.Id)},
                    {TagTable.UserId} AS {nameof(Tag.UserId)},
                    {TagTable.TagName} AS {nameof(Tag.Name)}                    
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleAsync<Tag>(query, new {UserId = userId, TagName = name});
        }
        return await connection.QuerySingleAsync<Tag>(query, new {UserId = userId, TagName = name});
    }

    public async Task<Tag> GetById(int id, IDbConnection connection, IDbTransaction? transaction = null)
    {
        var query =
            $@"
                SELECT
                    {TagTable.Id} AS {nameof(Tag.Id)},
                    {TagTable.UserId} AS {nameof(Tag.UserId)},
                    {TagTable.TagName} AS {nameof(Tag.Name)}
                FROM {SchemaDescriptor.SchemaName}.{TagTable.TableName}
                WHERE {TagTable.Id} = @Id
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleOrDefaultAsync<Tag>(query, new {Id = id}) ?? throw new NotFoundException($"No tag with id {id}");
        }
        return await connection.QuerySingleOrDefaultAsync<Tag>(query, new {Id = id}) ?? throw new NotFoundException($"No tag with id {id}");
    }

    public async Task<IEnumerable<Tag>> GetUserTags(int userId, IDbConnection connection, IDbTransaction? transaction = null)
    {
        var query =
            $@"
                SELECT
                    {TagTable.Id} AS {nameof(Tag.Id)},
                    {TagTable.UserId} AS {nameof(Tag.UserId)},
                    {TagTable.TagName} AS {nameof(Tag.Name)}
                FROM {SchemaDescriptor.SchemaName}.{TagTable.TableName}
                WHERE {TagTable.UserId} = @UserId
            ";

        if (transaction is not null)
        {
            return await transaction.QueryAsync<Tag>(query, new {UserId = userId});
        }
        return await connection.QueryAsync<Tag>(query, new {UserId = userId});
    }

    public async Task<Tag> Update(int id, Tag newValue, IDbConnection connection, IDbTransaction? transaction = null)
    {
        var query =
            $@"
                UPDATE {SchemaDescriptor.SchemaName}.{TagTable.TableName}
                SET {TagTable.TagName} = @TagName
                WHERE {TagTable.Id} = @Id
                RETURNING
                    {TagTable.Id} AS {nameof(Tag.Id)},
                    {TagTable.UserId} AS {nameof(Tag.UserId)},
                    {TagTable.TagName} AS {nameof(Tag.Name)}  
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleOrDefaultAsync<Tag>(query, new {Id = id, TagName = newValue.Name}) ??
                   throw new NotFoundException($"No tag with such id: {id}");
        }
        
        return await connection.QuerySingleOrDefaultAsync<Tag>(query, new {Id = id, TagName = newValue.Name}) ??
               throw new NotFoundException($"No tag with such id: {id}");
    }

    public async Task Delete(int id, IDbConnection connection, IDbTransaction? transaction = null)
    {
        var query =
            $@"
                DELETE 
                FROM {SchemaDescriptor.SchemaName}.{TagTable.TableName}
                WHERE {TagTable.Id} = @Id
            ";

        if (transaction is not null)
        {
            await transaction.ExecuteAsync(query, new {Id = id});
            return;
        }
        await connection.ExecuteAsync(query, new {Id = id});
    }
}