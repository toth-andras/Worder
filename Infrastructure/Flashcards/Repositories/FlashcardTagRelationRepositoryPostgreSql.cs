// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using System.Data;
using Application.Flashcards.Repositories;
using Dapper;
using Dapper.Transaction;
using Domain.Flashcards;
using Domain.ModelsForDataBase;
using Migrations.Descriptors;

namespace Infrastructure.Flashcards.Repositories;

public class FlashcardTagRelationRepositoryPostgreSql : IFlashcardTagRelationRepository
{
    public async Task CreateRelation(int flashcardId, int tagId, IDbConnection connection, IDbTransaction? transaction = null)
    {
        var query =
            $@"
                INSERT 
                INTO {SchemaDescriptor.SchemaName}.{FlashcardToTagTable.TableName}
                    ({FlashcardToTagTable.FlashcardId}, {FlashcardToTagTable.TagId})
                VALUES (@FlashcardId, @TagId)
            ";

        if (transaction is not null)
        {
            await transaction.ExecuteAsync(query, new {FlashcardId = flashcardId, TagId = tagId});
            return;
        }
        await connection.ExecuteAsync(query, new {FlashcardId = flashcardId, TagId = tagId});
    }

    public async Task<IEnumerable<Tag>> GetFlashcardTags(int flashcardId, IDbConnection connection, IDbTransaction? transaction = null)
    {
        var query =
            $@"
                SELECT
                    {TagTable.TableName}.{TagTable.Id} AS {nameof(Tag.Id)},
                    {TagTable.TableName}.{TagTable.UserId} AS {nameof(Tag.UserId)},
                    {TagTable.TableName}.{TagTable.TagName} AS {nameof(Tag.Name)}
                FROM 
                    {SchemaDescriptor.SchemaName}.{FlashcardToTagTable.TableName} JOIN {SchemaDescriptor.SchemaName}.{TagTable.TableName} 
                        ON {FlashcardToTagTable.TableName}.{FlashcardToTagTable.TagId}={TagTable.TableName}.{TagTable.Id}
                WHERE {FlashcardToTagTable.TableName}.{FlashcardToTagTable.FlashcardId}=@FlashcardId
            ";
        
        if (transaction is not null)
        {
            return await transaction.QueryAsync<Tag>(query, new {FlashcardId = flashcardId});
        }
        return await connection.QueryAsync<Tag>(query, new {FlashcardId = flashcardId});
    }

    public async Task DeleteRelation(int flashcardId, int tagId, IDbConnection connection, IDbTransaction? transaction = null)
    {
        var query =
            $@"
                DELETE 
                FROM {SchemaDescriptor.SchemaName}.{FlashcardToTagTable.TableName}
                WHERE {FlashcardToTagTable.FlashcardId} = @FlashcardId AND {FlashcardToTagTable.TagId} = @TagId
            ";
        
        if (transaction is not null)
        {
            await transaction.ExecuteAsync(query, new {FlashcardId = flashcardId, TagId = tagId});
            return;
        }
        await connection.ExecuteAsync(query, new {FlashcardId = flashcardId, TagId = tagId});
    }
}