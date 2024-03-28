// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using System.Data;
using Application.FlashcardStatistics.Services;
using Application.Repositories;
using FlashcardStatisticsClass = Domain.Flashcards.FlashcardStatistics;

namespace Infrastructure.Flashcards.FlashcardStatistics.Services;

public class FlashcardStatisticsService : IFlashcardStatisticsService
{
    private readonly IFlashcardStatisticsRepository _repository;
    private readonly IDbConnection _dbConnection;

    public FlashcardStatisticsService(IFlashcardStatisticsRepository repository, IDbConnection connection)
    {
        _repository = repository;
        _dbConnection = connection;
    }
    
    public async Task<FlashcardStatisticsClass> CreateStatistics(int flashcardId)
    {
        return await _repository.Create(flashcardId, _dbConnection);
    }

    public async Task<FlashcardStatisticsClass> GetFlashcardStatistics(int flashcardId)
    {
        return await _repository.GetByFlashcardId(flashcardId, _dbConnection);
    }

    public async Task<FlashcardStatisticsClass> OnRevisedCorrectly(int flashcardId)
    {
        var statistics = await _repository.GetByFlashcardId(flashcardId, _dbConnection);
        statistics.LastAnswerCorrect = true;
        statistics.LastTimeRevisedUtc = DateTime.UtcNow;
        statistics.FlashCardBox = Math.Min(statistics.FlashCardBox + 1, FlashcardStatisticsClass.MaxFlashCardBox);

        return await _repository.Update(flashcardId, statistics, _dbConnection);
    }

    public async Task<FlashcardStatisticsClass> OnRevisedIncorrectly(int flashcardId)
    {
        var statistics = await _repository.GetByFlashcardId(flashcardId, _dbConnection);
        statistics.LastAnswerCorrect = false;
        statistics.LastTimeRevisedUtc = DateTime.UtcNow;
        statistics.FlashCardBox = Math.Max(statistics.FlashCardBox - 1, FlashcardStatisticsClass.MinFlashCardBox);

        return await _repository.Update(flashcardId, statistics, _dbConnection);
    }

    public async Task DeleteStatistics(int flashcardId)
    {
        await _repository.Delete(flashcardId, _dbConnection);
    }
}