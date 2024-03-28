// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using System.Data;
using Domain.Flashcards;

namespace Application.Flashcards.Repositories;

public interface IFlashcardTagRelationRepository
{
    public Task CreateRelation(int flashcardId, int tagId, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<IEnumerable<Tag>> GetFlashcardTags(int flashcardId, IDbConnection connection, IDbTransaction? transaction = null);

    public Task DeleteRelation(int flashcardId, int tagId, IDbConnection connection,
        IDbTransaction? transaction = null);
}