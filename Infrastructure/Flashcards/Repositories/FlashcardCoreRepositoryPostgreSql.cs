// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 26.3.2024

using System.Data;
using Application.Exceptions;
using Application.Flashcards.Repositories;
using Dapper;
using Dapper.Transaction;
using Domain.Flashcards;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Infrastructure.Flashcards.Repositories;

public class FlashcardCoreRepositoryPostgreSql: IFlashcardCoreRepository
{
    public async Task<Flashcard> Create(int userId, string term, string definition, IDbConnection connection, IDbTransaction? transaction=null)
    {
        var query =
            $@"
                INSERT 
                INTO {SchemaDescriptor.SchemaName}.{FlashcardTable.TableName} 
                ({FlashcardTable.UserId}, {FlashcardTable.Term}, {FlashcardTable.Definition})
                VALUES (@UserId, @Term, @Definition)
                RETURNING 
                    {FlashcardTable.Id} AS {nameof(Flashcard.Id)},
                    {FlashcardTable.UserId} AS {nameof(Flashcard.UserId)},
                    {FlashcardTable.Term} AS {nameof(Flashcard.Term)},
                    {FlashcardTable.Definition} AS {nameof(Flashcard.Definition)}
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleAsync<Flashcard>(query,
                new {UserId = userId, Term = term, Definition = definition});
        }

        return await connection.QuerySingleAsync<Flashcard>(query,
            new {UserId = userId, Term = term, Definition = definition});
    }

    public async Task<Flashcard> GetById(int id, IDbConnection connection, IDbTransaction? transaction=null)
    {
        var query =
            $@"
                SELECT 
                    {FlashcardTable.Id} AS {nameof(Flashcard.Id)},
                    {FlashcardTable.UserId} AS {nameof(Flashcard.UserId)},
                    {FlashcardTable.Term} AS {nameof(Flashcard.Term)},
                    {FlashcardTable.Definition} AS {nameof(Flashcard.Definition)}
                FROM {SchemaDescriptor.SchemaName}.{FlashcardTable.TableName}
                WHERE {FlashcardTable.Id} = @Id
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleOrDefaultAsync<Flashcard>(query, new {Id = id}) ??
                   throw new NotFoundException($"No flashcard with such id: {id}");
        }
        
        return await connection.QuerySingleOrDefaultAsync<Flashcard>(query, new {Id = id}) ??
               throw new NotFoundException($"No flashcard with such id: {id}");
    }

    public async Task<IEnumerable<Flashcard>> GetByUserId(int userId, IDbConnection connection, IDbTransaction? transaction)
    {
        var query =
            $@"
                SELECT 
                    {FlashcardTable.Id} AS {nameof(Flashcard.Id)},
                    {FlashcardTable.UserId} AS {nameof(Flashcard.UserId)},
                    {FlashcardTable.Term} AS {nameof(Flashcard.Term)},
                    {FlashcardTable.Definition} AS {nameof(Flashcard.Definition)}
                FROM {SchemaDescriptor.SchemaName}.{FlashcardTable.TableName}
                WHERE {FlashcardTable.UserId} = @UserId
            ";

        if (transaction is not null)
        {
            return await transaction.QueryAsync<Flashcard>(query, new {UserId = userId});
        }

        return await connection.QueryAsync<Flashcard>(query, new {UserId = userId});
    }

    public async Task<Flashcard> Update(int id, Flashcard newValue, IDbConnection connection, IDbTransaction? transaction=null)
    {
        var query =
            $@"
                UPDATE {SchemaDescriptor.SchemaName}.{FlashcardTable.TableName}
                SET 
                    {FlashcardTable.Term} = @NewTerm,
                    {FlashcardTable.Definition} = @NewDefinition
                WHERE {FlashcardTable.Id} = @Id
                RETURNING 
                    {FlashcardTable.Id} AS {nameof(Flashcard.Id)},
                    {FlashcardTable.UserId} AS {nameof(Flashcard.UserId)},
                    {FlashcardTable.Term} AS {nameof(Flashcard.Term)},
                    {FlashcardTable.Definition} AS {nameof(Flashcard.Definition)}
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleOrDefaultAsync<Flashcard>(query,
                       new {Id = id, NewTerm = newValue.Term, NewDefinition = newValue.Definition}) ??
                   throw new NotFoundException($"No flashcard with such id: {id}");
        }

        return await connection.QuerySingleOrDefaultAsync<Flashcard>(query,
                   new {Id = id, NewTerm = newValue.Term, NewDefinition = newValue.Definition}) ??
               throw new NotFoundException($"No flashcard with such id: {id}");
    }

    public async Task Delete(int id, IDbConnection connection, IDbTransaction? transaction=null)
    {
        var query =
            $@"
                DELETE 
                FROM {SchemaDescriptor.SchemaName}.{FlashcardTable.TableName}
                WHERE {FlashcardTable.Id} = @Id;
            ";

        if (transaction is not null)
        {
            await transaction.ExecuteAsync(query, new {Id = id});
            return;
        }

        await connection.ExecuteAsync(query, new {Id = id});
    }
}