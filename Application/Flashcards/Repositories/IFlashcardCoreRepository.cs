// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 26.3.2024

using System.Data;
using Domain.Flashcards;

namespace Application.Flashcards.Repositories;

public interface IFlashcardCoreRepository
{
    public Task<Flashcard> Create(int userId, string term, string definition, IDbConnection connection,
        IDbTransaction? transaction = null);

    public Task<Flashcard> GetById(int id, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<IEnumerable<Flashcard>> GetByUserId(int userId, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<Flashcard> Update(int id, Flashcard newValue, IDbConnection connection,
        IDbTransaction? transaction = null);

    public Task Delete(int id, IDbConnection connection, IDbTransaction? transaction=null);
}