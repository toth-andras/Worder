// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using Application.FlashcardStatistics.Services.ForgetfulnessProbability;
using Domain.Flashcards;
using FlashcardStatisticsClass = Domain.Flashcards.FlashcardStatistics;

namespace Infrastructure.Flashcards.FlashcardStatistics.Services.ForgetfulnessProbability;

public class EbbinghausForgetfulnessProbabilityService : IForgetfulnessProbabilityService
{
    public double GetForgetfulnessProbability(Flashcard flashcard, FlashcardStatisticsClass statistics)
    {
        if (statistics.LastTimeRevisedUtc is null)
        {
            return 1; // If the user has never revised the flashcard, then the forgetfulness probability is the highest.
        }
        
        const double k = 1.84;
        const double c = 1.25;
        double minutesPassed = (DateTime.UtcNow - statistics.LastTimeRevisedUtc).Value.TotalDays;

        return 100 * k / (Math.Pow(Math.Log(minutesPassed), c) + k);
    }
}