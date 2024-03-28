// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using Domain.Flashcards;

namespace Application.Flashcards.FlashcardSetCollectors.Revising;

public interface IRevisionFlashcardSetCollector
{
    public Task<IEnumerable<Flashcard>> GetFlashcardsForRevision(int userId, int amount);
}