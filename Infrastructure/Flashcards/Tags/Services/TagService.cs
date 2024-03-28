// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using System.Data;
using Application.Exceptions;
using Application.Repositories;
using Application.Services;
using Domain.Flashcards;

namespace Infrastructure.Flashcards.Tags.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IDbConnection _dbConnection;

    public TagService(ITagRepository repository, IDbConnection connection)
    {
        _tagRepository = repository;
        _dbConnection = connection;
    }
    
    public async Task<Tag> Create(int userId, string name)
    {
        var tagNames = (await _tagRepository.GetUserTags(userId, _dbConnection))
            .Select(x => x.Name)
            .ToHashSet();

        if (tagNames.Contains(name))
        {
            throw new TagNameCollisionException($"A tag with name {name} has been already added");
        }

        return await _tagRepository.Create(userId, name, _dbConnection);
    }

    public async Task<Tag> GetById(int id)
    {
        return await _tagRepository.GetById(id, _dbConnection);
    }

    public async Task<IEnumerable<Tag>> GetUserTags(int userId)
    {
        return await _tagRepository.GetUserTags(userId, _dbConnection);
    }

    public async Task<Tag> Update(int id, int userId, Tag newValue)
    {
        var tagNames = (await _tagRepository.GetUserTags(userId, _dbConnection))
            .Select(x => x.Name)
            .ToHashSet();
        var oldName = (await _tagRepository.GetById(id, _dbConnection)).Name;

        if (tagNames.Contains(newValue.Name) && newValue.Name != oldName)
        {
            throw new TagNameCollisionException($"A tag with name {newValue.Name} has been already added");
        }
        
        return await _tagRepository.Update(id, newValue, _dbConnection);
    }

    public async Task Delete(int id)
    {
        await _tagRepository.Delete(id, _dbConnection);
    }
}