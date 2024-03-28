// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using Application.Flashcards.FlashcardSetCollectors.Revising;
using Application.Flashcards.Services;
using Application.FlashcardStatistics.Services;
using Application.FlashcardStatistics.Services.ForgetfulnessProbability;
using Domain.Flashcards;
using FlashcardStatisticsClass = Domain.Flashcards.FlashcardStatistics;

namespace Infrastructure.Flashcards.FlashcardSetCollectors;

public class RevisionFlashcardSetCollector : IRevisionFlashcardSetCollector
{
    private readonly IFlashcardService _flashcardService;
    private readonly IForgetfulnessProbabilityService _forgetfulnessService;
    private readonly IFlashcardStatisticsService _statisticsService;
    
    public RevisionFlashcardSetCollector(IFlashcardService flashcardService, IForgetfulnessProbabilityService forgetfulnessService, IFlashcardStatisticsService statisticsService)
    {
        _flashcardService = flashcardService;
        _forgetfulnessService = forgetfulnessService;
        _statisticsService = statisticsService;
    }
    
    public async Task<IEnumerable<Flashcard>> GetFlashcardsForRevision(int userId, int amount)
    {
        var flashcards = await _flashcardService.GetUserFlashcards(userId);
        List<(Flashcard flashcard, FlashcardStatisticsClass statistics, double forgetfullnessProbability)> revisionCandidates = new ();
        
        foreach (var flashcard in flashcards)
        {
            var statistics = await _statisticsService.GetFlashcardStatistics(flashcard.Id);
            revisionCandidates.Add(
                (flashcard, statistics, _forgetfulnessService.GetForgetfulnessProbability(flashcard, statistics)));
        }

        return revisionCandidates
            .OrderBy(x => x.forgetfullnessProbability)
            .ThenBy(x => x.statistics.LastAnswerCorrect ?? false)
            .Select(x => x.flashcard)
            .Take(amount);
    }
}