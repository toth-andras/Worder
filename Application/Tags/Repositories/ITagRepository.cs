// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using Domain;
using Application.ReturnTypes;
using FluentValidation.Results;
using OneOf;

namespace Application.Repositories;

/// <summary>
/// Represents a common interface for all Tag repositories.
/// </summary>
public interface ITagRepository
{
    /// <summary>
    /// Gets the tag from the repository.
    /// </summary>
    /// <param name="id">The id of the tag to get.</param>
    /// <returns>
    /// The tag with the given id or <see cref="NotFound"/>,
    /// if the tag is not present in the repository
    /// </returns>
    public OneOf<Tag, NotFound> GetById(int id);

    /// <summary>
    /// Returns all the tags that the user owns from the repository.
    /// </summary>
    /// <param name="userId">user's id</param>
    /// <returns>A collection containing user's tags</returns>
    public IEnumerable<Tag> GetByUserId(int userId);

    /// <summary>
    /// Returns all the tags stored in the repository.
    /// </summary>
    /// <returns>A collection containing all tags stored in the repository</returns>
    public IEnumerable<Tag> GetAll();

    /// <summary>
    /// Adds a tag to the repository.
    /// </summary>
    /// <param name="userId">Id of the user the tag belongs to</param>
    /// <param name="tag">The tag</param>
    /// <returns>
    /// <see cref="Success"/> if the addition was executed successfully or <see cref="ValidationFailure"/>
    /// if the given tag did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Add(int userId, Tag tag);

    /// <summary>
    /// Updates the data that represents the tag in repository to match the new state.
    /// </summary>
    /// <param name="tag">A tag in its new state</param>
    /// <returns>
    /// <see cref="Success"/> if the update was executed successfully or <see cref="ValidationFailure"/>
    /// if the new state of the tag did not pass validation.
    /// </returns>
    public OneOf<Success, ValidationFailed> Update(Tag tag);

    /// <summary>
    /// Removes a tag from the repository.
    /// </summary>
    /// <param name="id">The id of the tag to remove</param>
    /// <returns>
    /// <see cref="Success"/> if the removal was executed successfully and <see cref="NotFound"/>,
    /// if the tag is not present in the repository
    /// </returns>
    public OneOf<Success, NotFound> Remove(int id);
}