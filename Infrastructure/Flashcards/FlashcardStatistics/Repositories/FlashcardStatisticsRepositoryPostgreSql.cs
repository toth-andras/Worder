// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using System.Data;
using Application.Exceptions;
using Application.Repositories;
using Dapper;
using Dapper.Transaction;
using Migrations.Descriptors;
using FlashcardStatisticsClass = Domain.Flashcards.FlashcardStatistics;

namespace Infrastructure.Flashcards.FlashcardStatistics.Repositories;

public class FlashcardStatisticsRepositoryPostgreSql : IFlashcardStatisticsRepository
{
    public async Task<FlashcardStatisticsClass> Create(int flashcardId, IDbConnection connection,
        IDbTransaction? transaction = null)
    {
        var query =
            $@"
                INSERT INTO {SchemaDescriptor.SchemaName}.{FlashcardStatisticsTable.TableName} ({FlashcardStatisticsTable.FlashcardId})
                VALUES (@FlashcardId)
                RETURNING
                    {FlashcardStatisticsTable.Id} AS {nameof(FlashcardStatisticsClass.Id)}, 
                    {FlashcardStatisticsTable.FlashcardId} AS {nameof(FlashcardStatisticsClass.FlashcardId)}, 
                    {FlashcardStatisticsTable.LastTimeRevisedUtc} AS {nameof(FlashcardStatisticsClass.LastTimeRevisedUtc)}, 
                    {FlashcardStatisticsTable.LastAnswerCorrect} AS {nameof(FlashcardStatisticsClass.LastAnswerCorrect)}, 
                    {FlashcardStatisticsTable.FlashcardBox} AS {nameof(FlashcardStatisticsClass.FlashCardBox)}
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleAsync<FlashcardStatisticsClass>(query, new {FlashcardId = flashcardId});
        }

        return await connection.QuerySingleAsync<FlashcardStatisticsClass>(query, new {FlashcardId = flashcardId});
    }

    public async Task<FlashcardStatisticsClass> GetByFlashcardId(int flashcardId, IDbConnection connection,
        IDbTransaction? transaction = null)
    {
        var query =
            $@"
                SELECT
                    {FlashcardStatisticsTable.Id} AS {nameof(FlashcardStatisticsClass.Id)}, 
                    {FlashcardStatisticsTable.FlashcardId} AS {nameof(FlashcardStatisticsClass.FlashcardId)}, 
                    {FlashcardStatisticsTable.LastTimeRevisedUtc} AS {nameof(FlashcardStatisticsClass.LastTimeRevisedUtc)}, 
                    {FlashcardStatisticsTable.LastAnswerCorrect} AS {nameof(FlashcardStatisticsClass.LastAnswerCorrect)}, 
                    {FlashcardStatisticsTable.FlashcardBox} AS {nameof(FlashcardStatisticsClass.FlashCardBox)}
                FROM {SchemaDescriptor.SchemaName}.{FlashcardStatisticsTable.TableName}
                WHERE {FlashcardStatisticsTable.FlashcardId} = @FlashcardId
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleOrDefaultAsync<FlashcardStatisticsClass>(query,
                       new {FlashcardId = flashcardId}) ??
                   throw new NotFoundException($"No statistics for flashcard with id {flashcardId}");
        }

        return await connection.QuerySingleOrDefaultAsync<FlashcardStatisticsClass>(query, new {FlashcardId = flashcardId}) ??
               throw new NotFoundException($"No statistics for flashcard with id {flashcardId}");
    }

    public async Task<FlashcardStatisticsClass> Update(int flashcardId, FlashcardStatisticsClass newValue,
        IDbConnection connection, IDbTransaction? transaction = null)
    {
        var query =
            $@"
                UPDATE {SchemaDescriptor.SchemaName}.{FlashcardStatisticsTable.TableName}
                SET
                    {FlashcardStatisticsTable.LastTimeRevisedUtc} = @LastTimeRevisedUtc, 
                    {FlashcardStatisticsTable.LastAnswerCorrect} = @LastAnswerCorrect, 
                    {FlashcardStatisticsTable.FlashcardBox} = @FlashcardBox
                WHERE {FlashcardStatisticsTable.FlashcardId} = @FlashcardId
                RETURNING
                    {FlashcardStatisticsTable.Id} AS {nameof(FlashcardStatisticsClass.Id)}, 
                    {FlashcardStatisticsTable.FlashcardId} AS {nameof(FlashcardStatisticsClass.FlashcardId)}, 
                    {FlashcardStatisticsTable.LastTimeRevisedUtc} AS {nameof(FlashcardStatisticsClass.LastTimeRevisedUtc)}, 
                    {FlashcardStatisticsTable.LastAnswerCorrect} AS {nameof(FlashcardStatisticsClass.LastAnswerCorrect)}, 
                    {FlashcardStatisticsTable.FlashcardBox} AS {nameof(FlashcardStatisticsClass.FlashCardBox)}
            ";

        if (transaction is not null)
        {
            return await transaction.QuerySingleOrDefaultAsync<FlashcardStatisticsClass>(query, new
            {
                LastTimeRevisedUtc = newValue.LastTimeRevisedUtc,
                LastAnswerCorrect = newValue.LastAnswerCorrect,
                FlashcardBox = newValue.FlashCardBox,
                FlashcardId = flashcardId
            }) ?? throw new NotFoundException($"No statistics for flashcard with id {flashcardId}");
        }

        return await connection.QuerySingleOrDefaultAsync<FlashcardStatisticsClass>(query, new
        {
            LastTimeRevisedUtc = newValue.LastTimeRevisedUtc,
            LastAnswerCorrect = newValue.LastAnswerCorrect,
            FlashcardBox = newValue.FlashCardBox,
            FlashcardId = flashcardId
        }) ?? throw new NotFoundException($"No statistics for flashcard with id {flashcardId}");
    }

    public async Task Delete(int flashcardId, IDbConnection connection, IDbTransaction? transaction = null)
    {
        var query =
            $@"
                DELETE FROM {SchemaDescriptor.SchemaName}.{FlashcardStatisticsTable.TableName}
                WHERE {FlashcardStatisticsTable.FlashcardId} = @FlashcardId
            ";

        if (transaction is not null)
        {
            await transaction.ExecuteAsync(query, new {FlashcardId = flashcardId});
            return;
        }

        await connection.ExecuteAsync(query, new {FlashcardId = flashcardId});
    }
}