// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 26.3.2024

using Domain.Flashcards;

namespace Application.Flashcards.Repositories;

public interface IFlashcardRepository
{
    public Flashcard Create(int userId, string term, string definition);

    public Flashcard GetById(int id);

    public IEnumerable<Flashcard> GetByUserEmail(string email);

    public Flashcard Update(int id, Flashcard newValue);
}