// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using Domain.Flashcards;

namespace Application.Services;

public interface ITagService
{
    public Task<Tag> Create(int userId, string name);

    public Task<Tag> GetById(int id);
    
    public Task<IEnumerable<Tag>> GetUserTags(int userId);

    public Task<Tag> Update(int id, int userId, Tag newValue);

    public Task Delete(int id);
}