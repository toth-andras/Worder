// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

using Domain.Flashcards;

namespace Application.Flashcards.FlashcardFields.Repositories;

public interface ITextFlashcardFieldRepository
{
    public Task<TextFlashcardField> Create(int flashcardId, string fieldName, bool canBeShownInQuestion, string fieldValue);

    public Task<TextFlashcardField> GetById(int id);

    public Task<IEnumerable<TextFlashcardField>> GetFlashcardTextFields(int flashcardId);

    public Task<TextFlashcardField> Update(int id, TextFlashcardField newValue);

    public Task Delete(int id);
}