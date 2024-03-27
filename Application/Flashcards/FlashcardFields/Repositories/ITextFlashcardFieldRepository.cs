// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

using System.Data;
using Domain.Flashcards;

namespace Application.Flashcards.FlashcardFields.Repositories;

public interface ITextFlashcardFieldRepository
{
    public Task<TextFlashcardField> Create(int flashcardId, string fieldName, bool canBeShownInQuestion,
        string fieldValue, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<TextFlashcardField> GetById(int id, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<IEnumerable<TextFlashcardField>> GetFlashcardTextFields(int flashcardId, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<TextFlashcardField> Update(int id, TextFlashcardField newValue, IDbConnection connection, IDbTransaction? transaction=null);

    public Task Delete(int id, IDbConnection connection, IDbTransaction? transaction=null);
}