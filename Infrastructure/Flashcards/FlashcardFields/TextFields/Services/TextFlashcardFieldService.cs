// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

using System.Data;
using Application.Flashcards.FlashcardFields.Repositories;
using Application.Flashcards.FlashcardFields.Services;
using Domain.Flashcards;

namespace Infrastructure.Flashcards.FlashcardFields.TextFields.Services;

public class TextFlashcardFieldService : ITextFlashcardFieldService
{
    private readonly ITextFlashcardFieldRepository _repository;
    private readonly IDbConnection _dbConnection;

    public TextFlashcardFieldService(ITextFlashcardFieldRepository repository, IDbConnection connection)
    {
        _repository = repository;
        _dbConnection = connection;
    }
    
    public async Task<TextFlashcardField> CreateTextField(int flashcardId, string fieldName, bool canBeShownInQuestion, string fieldValue)
    {
        return await _repository.Create(flashcardId, fieldName, canBeShownInQuestion, fieldValue, _dbConnection);
    }

    public async Task<TextFlashcardField> GetTextFieldById(int id)
    {
        return await _repository.GetById(id, _dbConnection);
    }

    public async Task<IEnumerable<TextFlashcardField>> GetFlashcardTextFields(int flashcardId)
    {
        return await _repository.GetFlashcardTextFields(flashcardId, _dbConnection);
    }

    public async Task<TextFlashcardField> UpdateTextFields(int id, TextFlashcardField newValue)
    {
        return await _repository.Update(id, newValue, _dbConnection);
    }

    public async Task DeleteTextField(int id)
    {
        await _repository.Delete(id, _dbConnection);
    }
}