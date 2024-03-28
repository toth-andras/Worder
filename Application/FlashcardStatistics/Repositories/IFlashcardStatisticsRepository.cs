// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using System.Data;
using OneOf;
using Domain.Flashcards;
using Application.ReturnTypes;

namespace Application.Repositories;

/// <summary>
/// Represents a common interface for all flashcard statistics repositories.
/// </summary>
public interface IFlashcardStatisticsRepository
{
    public Task<FlashcardStatistics> Create(int flashcardId, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<FlashcardStatistics> GetByFlashcardId(int flashcardId, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<FlashcardStatistics> Update(int flashcardId, FlashcardStatistics newValue, IDbConnection connection,
        IDbTransaction? transaction=null);

    public Task Delete(int flashcardId, IDbConnection connection, IDbTransaction? transaction=null);
}