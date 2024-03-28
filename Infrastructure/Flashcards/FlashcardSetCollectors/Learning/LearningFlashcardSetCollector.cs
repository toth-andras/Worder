// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using Application.Flashcards.FlashcardSetCollectors.Learning;
using Application.Flashcards.Services;
using Application.FlashcardStatistics.Services;
using Domain.Flashcards;
using FlashcardStatisticsClass = Domain.Flashcards.FlashcardStatistics;

namespace Infrastructure.Flashcards.FlashcardSetCollectors.Learning;

public class LearningFlashcardSetCollector : ILearningFlashcardSetCollector
{
    private readonly IFlashcardService _flashcardService;
    private readonly IFlashcardStatisticsService _statisticsService;

    public LearningFlashcardSetCollector(IFlashcardService flashcardService, IFlashcardStatisticsService statisticsService)
    {
        _flashcardService = flashcardService;
        _statisticsService = statisticsService;
    }
    
    public async Task<IEnumerable<Flashcard>> GetFlashcardsForLearning(int userId, int amount)
    {
        var flashcards = await _flashcardService.GetUserFlashcards(userId);
        List<(Flashcard flashcard, FlashcardStatisticsClass statistics)> learningCandidates = [];

        foreach (var flashcard in flashcards)
        {
            learningCandidates.Add((flashcard, await _statisticsService.GetFlashcardStatistics(flashcard.Id)));
        }

        return learningCandidates
            .OrderBy(x => x.statistics.FlashCardBox)
            .ThenBy(x => x.statistics.LastTimeRevisedUtc ?? DateTime.MinValue)
            .Select(x => x.flashcard);
    }
}