// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using Application.ReturnTypes;
using Domain;
using OneOf;
using Success = OneOf.Types.Success;

namespace Application.Repositories;

/// <summary>
/// Represents a common interface for all flashcard field repositories.
/// </summary>
/// <typeparam name="T">The type of the fields stored in the repository</typeparam>
public interface IFlashcardFieldRepository<T> where T: FlashcardFieldBase
{
    /// <summary>
    /// Gets the flashcard field.
    /// </summary>
    /// <param name="id">flashcard field identifier</param>
    /// <returns>
    /// Flashcard or <see cref="NotFound"/> if no such flashcard field
    /// is presented in the repository
    /// </returns>
    public OneOf<T, NotFound> GetById(int id);
    
    /// <summary>
    /// Gets the collection of flashcard fields.
    /// </summary>
    /// <param name="ids">a collection of flashcard field identifiers</param>
    /// <returns>The collection of flashcard fields</returns>
    public IEnumerable<T> GetByIds(int[] ids);
    
    /// <summary>
    /// Gets all flashcard fields attached to the flashcard.
    /// </summary>
    /// <param name="flashcardId">flashcard identifier</param>
    /// <returns></returns>
    public IEnumerable<T> GetByFlashcardId(int flashcardId);
    
    /// <summary>
    /// Adds flashcard field.
    /// </summary>
    /// <param name="flashcardId">the identifier of the flashcard the
    /// flashcard field is attached to</param>
    /// <param name="field">flashcard field</param>
    /// <returns>
    /// <see cref="Success"/> if the addition was executed successfully or
    /// <see cref="ValidationFailed"/> if the flashcard field did not pass
    /// validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Add(int flashcardId, T field);
    
    /// <summary>
    /// Updates the data that represents the flashcard field to the provided state.
    /// </summary>
    /// <param name="field">the field with new state</param>
    /// <returns>
    /// <see cref="Success"/> if the update was executed successfully or
    /// <see cref="ValidationFailed"/> if the provided state did not pass
    /// validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Update(T field);
    
    /// <summary>
    /// Removes the flashcard field.
    /// </summary>
    /// <param name="id">flashcard field identifier</param>
    /// <returns>
    /// <see cref="Success"/> if the removal was executed successfully or
    /// <see cref="NotFound"/> if such a flashcard field is not presented
    /// in the repository
    /// </returns>
    public OneOf<Success, NotFound> Remove(int id);
}