// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using Domain;
using OneOf;
using Application.ReturnTypes;

namespace Application.Repositories;

/// <summary>
/// Represents a common interface for all flashcard repositories. 
/// </summary>
public interface IFlashcardRepository
{
    /// <summary>
    /// Gets the flashcard.
    /// </summary>
    /// <param name="id">flashcard identifier</param>
    /// <returns>
    /// The flashcard with the given identifier or <see cref="NotFound"/>
    /// such a flashcard is not present in the repository
    /// </returns>
    public OneOf<Flashcard, NotFound> GetById(int id);

    /// <summary>
    /// Returns all the flashcards owned by the user.
    /// </summary>
    /// <param name="userId">user's identifier</param>
    /// <returns>A collection containing user's flashcards</returns>
    public IEnumerable<Flashcard> GetByUserId(int userId);

    /// <summary>
    /// Gets the collection of flashcards.
    /// </summary>
    /// <param name="ids">a collection of flashcard identifiers</param>
    /// <returns>The collection of flashcards</returns>
    public IEnumerable<Flashcard> GetByIds(int[] ids);

    /// <summary>
    /// Gets all flashcards stored in the flashcard module.
    /// </summary>
    /// <param name="flashcardModuleId">flashcard module identifier</param>
    /// <returns>The collection of flashcards</returns>
    public IEnumerable<Flashcard> GetByFlashcardModuleId(int flashcardModuleId);

    /// <summary>
    /// Adds the flashcard to the repository.
    /// </summary>
    /// <param name="flashcard">the flashcard</param>
    /// <returns>
    /// <see cref="Success"/> if the addition was executed successfully or <see cref="ValidationFailed"/>
    /// if the given flashcard did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Add(Flashcard flashcard);

    /// <summary>
    /// Updates the data that represents the flashcard to the provided state.
    /// </summary>
    /// <param name="flashcard">the tag with new state</param>
    /// <returns>
    /// <see cref="Success"/> if the update was executed successfully or <see cref="ValidationFailed"/>
    /// if the new state of the flashcard did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Update(Flashcard flashcard);

    /// <summary>
    /// Removes the flashcard.
    /// </summary>
    /// <param name="id">flashcard identifier</param>
    /// <returns>
    /// <see cref="Success"/> if the removal was executed successfully or <see cref="NotFound"/> if
    /// such a flashcard is not present in the repository
    /// </returns>
    public OneOf<Success, NotFound> Remove(int id);
}