// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using Application.ReturnTypes;
using Domain;
using OneOf;

namespace Application.FlashcardModules;

/// <summary>
/// Represents a common interface for all flashcard module repositories.
/// </summary>
public interface IFlashcardModuleRepository
{
    /// <summary>
    /// Gets the flashcard module.
    /// </summary>
    /// <param name="id">the identifier of the flashcard module.</param>
    /// <returns>
    /// Flashcard module with the provided identifier or <see cref="NotFound"/>
    /// if such a flashcard module is not present in the repository
    /// </returns>
    public OneOf<FlashcardModule, NotFound> GetById(int id);
    
    /// <summary>
    /// Gets the collection of flashcard modules.
    /// </summary>
    /// <param name="ids">a collection of flashcard module identifiers</param>
    /// <returns>A collection of flashcard modules</returns>
    public IEnumerable<FlashcardModule> GetByIds(int[] ids);
    
    /// <summary>
    /// Gets all flashcard modules owned by the user.
    /// </summary>
    /// <param name="userId">user's identifier</param>
    /// <returns>A collection of flashcard modules</returns>
    public IEnumerable<FlashcardModule> GetByUserId(int userId);
    
    /// <summary>
    /// Adds flashcard module to the repository.
    /// </summary>
    /// <param name="userId">flashcard module owner's identifier</param>
    /// <param name="module">flashcard module</param>
    /// <returns>
    /// <see cref="Success"/> if the addition was executed successfully or
    /// <see cref="ValidationFailed"/> if the flashcard module did not pass
    /// validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Add(int userId, FlashcardModule module);
    
    /// <summary>
    /// Updates the data that represents the flashcard module to the provided state.
    /// </summary>
    /// <param name="module">the module with new state</param>
    /// <returns>
    /// <see cref="Success"/> if the update was executed successfully or
    /// <see cref="ValidationFailed"/> if the provided state did not pass
    /// validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Update(FlashcardModule module);
    
    /// <summary>
    /// Removes the flashcard module.
    /// </summary>
    /// <param name="id">flashcard module identifier</param>
    /// <returns>
    /// <see cref="Success"/> if the removal was executed successfully or
    /// <see cref="NotFound"/> if such a flashcard module is not presented
    /// in the repository
    /// </returns>
    public OneOf<Success, NotFound> Remove(int id);
}