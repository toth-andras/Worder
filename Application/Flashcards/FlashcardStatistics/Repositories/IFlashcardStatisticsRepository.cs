// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using System.Data;
using FlashcardStatisticsClass = Domain.Flashcards.FlashcardStatistics;

namespace Application.Repositories;

/// <summary>
/// Represents a common interface for all flashcard statistics repositories.
/// </summary>
public interface IFlashcardStatisticsRepository
{
    public Task<FlashcardStatisticsClass> Create(int flashcardId, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<FlashcardStatisticsClass> GetByFlashcardId(int flashcardId, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<FlashcardStatisticsClass> Update(int flashcardId, FlashcardStatisticsClass newValue, IDbConnection connection,
        IDbTransaction? transaction=null);

    public Task Delete(int flashcardId, IDbConnection connection, IDbTransaction? transaction=null);
}