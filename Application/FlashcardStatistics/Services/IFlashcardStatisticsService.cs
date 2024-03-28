// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

namespace Application.FlashcardStatistics.Services;
using FlashcardStatisticsClass = Domain.Flashcards.FlashcardStatistics;

public interface IFlashcardStatisticsService
{
    public Task<FlashcardStatisticsClass> CreateStatistics(int flashcardId);

    public Task<FlashcardStatisticsClass> GetFlashcardStatistics(int flashcardId);

    public Task<FlashcardStatisticsClass> OnRevisedCorrectly(int flashcardId);

    public Task<FlashcardStatisticsClass> OnRevisedIncorrectly(int flashcardId);

    public Task DeleteStatistics(int flashcardId);
}