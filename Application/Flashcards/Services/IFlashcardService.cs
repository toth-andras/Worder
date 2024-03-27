// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 26.3.2024

using Domain.Flashcards;

namespace Application.Flashcards.Services;

public interface IFlashcardService
{
    public Task<Flashcard> CreateFlashcard(int userId, string term, string definition, IEnumerable<FlashcardFieldBase>? fields);

    public Task<Flashcard> GetFlashcard(int id);

    public Task<IEnumerable<Flashcard>> GetUserFlashcards(int userId);

    public Task<Flashcard> UpdateFlashcard(int id, Flashcard newValue);

    public Task DeleteFlashcard(int id);
}