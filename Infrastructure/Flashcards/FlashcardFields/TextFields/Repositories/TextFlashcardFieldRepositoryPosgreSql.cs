// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

using System.Data;
using Application.Exceptions;
using Application.Flashcards.FlashcardFields.Repositories;
using Dapper;
using Dapper.Transaction;
using Domain.Flashcards;
using Migrations.Descriptors;

namespace Infrastructure.Flashcards.FlashcardFields.TextFields.Repositories;

public class TextFlashcardFieldRepositoryPosgreSql : ITextFlashcardFieldRepository
{
    public async Task<TextFlashcardField> Create(int flashcardId, string fieldName, bool canBeShownInQuestion, string fieldValue, IDbConnection connection,
        IDbTransaction? transaction=null)
    {
        var query =
            $@"
                INSERT 
                INTO {SchemaDescriptor.SchemaName}.{TextFlashcardFieldTable.TableName}
                ({TextFlashcardFieldTable.FlashcardId}, {TextFlashcardFieldTable.FieldName}, {TextFlashcardFieldTable.Value}, {TextFlashcardFieldTable.CanBeShownInQuestion})
                VALUES (@FlashcardId, @Name, @Value, @CanBeShownInQuestion)
                RETURNING
                    {TextFlashcardFieldTable.Id} AS {nameof(TextFlashcardField.Id)},
                    {TextFlashcardFieldTable.FlashcardId} AS {nameof(TextFlashcardField.FlashcardId)},
                    {TextFlashcardFieldTable.FieldName} AS {nameof(TextFlashcardField.Name)},
                    {TextFlashcardFieldTable.Value} AS {nameof(TextFlashcardField.Value)},
                    {TextFlashcardFieldTable.CanBeShownInQuestion} AS {nameof(TextFlashcardField.CanBeShownInQuestion)}
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleAsync<TextFlashcardField>(query,
                new
                {
                    FlashcardId = flashcardId, Name = fieldName, Value = fieldValue,
                    CanBeShownInQuestion = canBeShownInQuestion
                });
        }
        
        return await connection.QuerySingleAsync<TextFlashcardField>(query,
            new
            {
                FlashcardId = flashcardId, Name = fieldName, Value = fieldValue,
                CanBeShownInQuestion = canBeShownInQuestion
            });
    }

    public async Task<TextFlashcardField> GetById(int id, IDbConnection connection, IDbTransaction? transaction=null)
    {
        var query = 
            $@"
                SELECT 
                    {TextFlashcardFieldTable.Id} AS {nameof(TextFlashcardField.Id)},
                    {TextFlashcardFieldTable.FlashcardId} AS {nameof(TextFlashcardField.FlashcardId)},
                    {TextFlashcardFieldTable.FieldName} AS {nameof(TextFlashcardField.Name)},
                    {TextFlashcardFieldTable.Value} AS {nameof(TextFlashcardField.Value)},
                    {TextFlashcardFieldTable.CanBeShownInQuestion} AS {nameof(TextFlashcardField.CanBeShownInQuestion)}
                FROM {SchemaDescriptor.SchemaName}.{TextFlashcardFieldTable.TableName}
                WHERE {TextFlashcardFieldTable.Id} = @Id
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleOrDefaultAsync<TextFlashcardField>(query, new {Id = id}) ??
                throw new NotFoundException($"No text field with such id: {id}");
        }
        
        return await connection.QuerySingleOrDefaultAsync<TextFlashcardField>(query, new {Id = id}) ??
               throw new NotFoundException($"No text field with such id: {id}");
    }

    public async Task<IEnumerable<TextFlashcardField>> GetFlashcardTextFields(int flashcardId, IDbConnection connection, IDbTransaction? transaction=null)
    {
        var query = 
            $@"
                SELECT 
                    {TextFlashcardFieldTable.Id} AS {nameof(TextFlashcardField.Id)},
                    {TextFlashcardFieldTable.FlashcardId} AS {nameof(TextFlashcardField.FlashcardId)},
                    {TextFlashcardFieldTable.FieldName} AS {nameof(TextFlashcardField.Name)},
                    {TextFlashcardFieldTable.Value} AS {nameof(TextFlashcardField.Value)},
                    {TextFlashcardFieldTable.CanBeShownInQuestion} AS {nameof(TextFlashcardField.CanBeShownInQuestion)}
                FROM {SchemaDescriptor.SchemaName}.{TextFlashcardFieldTable.TableName}
                WHERE {TextFlashcardFieldTable.FlashcardId} = @FlashcardId
            ";

        if (transaction is not null)
        {
            return await transaction.QueryAsync<TextFlashcardField>(query, new {FlashcardId = flashcardId});
        }
        return await connection.QueryAsync<TextFlashcardField>(query, new {FlashcardId = flashcardId});
    }

    public async Task<TextFlashcardField> Update(int id, TextFlashcardField newValue, IDbConnection connection, IDbTransaction? transaction=null)
    {
        var query =
            $@"
                UPDATE {SchemaDescriptor.SchemaName}.{TextFlashcardFieldTable.TableName}
                SET
                    {TextFlashcardFieldTable.FieldName} = @Name,
                    {TextFlashcardFieldTable.Value} = @Value,
                    {TextFlashcardFieldTable.CanBeShownInQuestion} = @CanBeShownInQuestion
                WHERE
                    {TextFlashcardFieldTable.Id} = @Id
                RETURNING
                    {TextFlashcardFieldTable.Id} AS {nameof(TextFlashcardField.Id)},
                    {TextFlashcardFieldTable.FlashcardId} AS {nameof(TextFlashcardField.FlashcardId)},
                    {TextFlashcardFieldTable.FieldName} AS {nameof(TextFlashcardField.Name)},
                    {TextFlashcardFieldTable.Value} AS {nameof(TextFlashcardField.Value)},
                    {TextFlashcardFieldTable.CanBeShownInQuestion} AS {nameof(TextFlashcardField.CanBeShownInQuestion)}
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleAsync<TextFlashcardField>(query,
                new
                {
                    Id = id, Name = newValue.Name, Value = newValue.Value,
                    CanBeShownInQuestion = newValue.CanBeShownInQuestion
                });
        }
        
        return await connection.QuerySingleAsync<TextFlashcardField>(query,
            new
            {
                Id = id, Name = newValue.Name, Value = newValue.Value,
                CanBeShownInQuestion = newValue.CanBeShownInQuestion
            });
    }

    public async Task Delete(int id, IDbConnection connection, IDbTransaction? transaction=null)
    {
        var query =
            $@"
                DELETE
                FROM {SchemaDescriptor.SchemaName}.{TextFlashcardFieldTable.TableName}
                WHERE {TextFlashcardFieldTable.Id} = @Id
            ";
        if (transaction is not null)
        {
            await transaction.ExecuteAsync(query, new {Id = id});
            return;
        }
        await connection.ExecuteAsync(query, new {Id = id});
    }
}