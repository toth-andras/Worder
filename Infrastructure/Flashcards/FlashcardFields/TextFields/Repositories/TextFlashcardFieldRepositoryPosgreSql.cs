// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

using Application.Flashcards.FlashcardFields.Repositories;
using Domain.Flashcards;

namespace Infrastructure.Flashcards.FlashcardFields.TextFields.Repositories;

public class TextFlashcardFieldRepositoryPosgreSql : ITextFlashcardFieldRepository
{
    public Task<TextFlashcardField> Create(int flashcardId, string fieldName, bool canBeShownInQuestion, string fieldValue)
    {
        throw new NotImplementedException();
    }

    public Task<TextFlashcardField> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TextFlashcardField>> GetFlashcardTextFields(int flashcardId)
    {
        throw new NotImplementedException();
    }

    public Task<TextFlashcardField> Update(int id, TextFlashcardField newValue)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}