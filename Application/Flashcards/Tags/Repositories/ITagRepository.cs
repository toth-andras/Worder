// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using System.Data;
using Domain.Flashcards;
using Application.ReturnTypes;
using FluentValidation.Results;
using OneOf;

namespace Application.Repositories;

/// <summary>
/// Represents a common interface for all Tag repositories.
/// </summary>
public interface ITagRepository
{
    public Task<Tag> Create(int userId, string name, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<Tag> GetById(int id, IDbConnection connection, IDbTransaction? transaction=null);
    
    public Task<IEnumerable<Tag>> GetUserTags(int userId, IDbConnection connection, IDbTransaction? transaction=null);

    public Task<Tag> Update(int id, Tag newValue, IDbConnection connection, IDbTransaction? transaction=null);

    public Task Delete(int id, IDbConnection connection, IDbTransaction? transaction=null);
}