// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using Domain.Flashcards;
using FlashcardStatisticsClass = Domain.Flashcards.FlashcardStatistics;

namespace Application.FlashcardStatistics.Services.ForgetfulnessProbability;

public interface IForgetfulnessProbabilityService
{
    public double GetForgetfulnessProbability(Flashcard flashcard, FlashcardStatisticsClass statistics);
}