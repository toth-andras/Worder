// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

using System.Data;
using Domain.Flashcards;

namespace Application.Flashcards.FlashcardFields.Services;

public interface ITextFlashcardFieldService
{
    public Task<TextFlashcardField> CreateTextField(int flashcardId, string fieldName, bool canBeShownInQuestion,
        string fieldValue);

    public Task<TextFlashcardField> GetTextFieldById(int id);

    public Task<IEnumerable<TextFlashcardField>> GetFlashcardTextFields(int flashcardId);

    public Task<TextFlashcardField> UpdateTextFields(int id, TextFlashcardField newValue);

    public Task DeleteTextField(int id);
}