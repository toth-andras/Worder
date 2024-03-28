// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using Application.ReturnTypes;
using Domain.FlashcardTemplates;
using OneOf;

namespace Application.Repositories;

/// <summary>
/// Represents a common interface for all flashcard template repositories.
/// </summary>
public interface FlashcardTemplateRepository
{
    /// <summary>
    /// Gets the flashcard template.
    /// </summary>
    /// <param name="id">flashcard template identifier</param>
    /// <returns>
    /// The flashcard template or <see cref="NotFound"/>,
    /// such a flashcard template is not present in the repository
    /// </returns>
    public OneOf<FlashcardTemplate, NotFound> GetById(int id);
    
    /// <summary>
    /// Gets the collection of flashcard templates.
    /// </summary>
    /// <param name="ids">a collection of flashcard template identifiers</param>
    /// <returns>The collection of flashcard templates</returns>
    public IEnumerable<FlashcardTemplate> GetByIds(int[] ids);
    
    /// <summary>
    /// Returns all the flashcard templates owned by the user.
    /// </summary>
    /// <param name="userId">user's identifier</param>
    /// <returns>A collection containing user's flashcard templates</returns>
    public IEnumerable<FlashcardTemplate> GetByUserId(int userId);

    /// <summary>
    /// Adds the flashcard template.
    /// </summary>
    /// <param name="userId">user's identifier</param>
    /// <param name="template">flashcard template</param>
    /// <returns>
    /// <see cref="Success"/> if the addition was executed successfully or <see cref="ValidationFailed"/>
    /// if the given flashcard template did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Add(int userId, FlashcardTemplate template);
    
    /// <summary>
    /// Updates the data that represents the flashcard template to the provided state.
    /// </summary>
    /// <param name="template">the flashcard template with new state</param>
    /// <returns>
    /// <see cref="Success"/> if the update was executed successfully or <see cref="ValidationFailed"/>
    /// if the new state of the flashcard template did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Update(FlashcardTemplate template);
    
    /// <summary>
    /// Removes the flashcard template.
    /// </summary>
    /// <param name="id">flashcard template identifier</param>
    /// <returns>
    /// <see cref="Success"/> if the removal was executed successfully or <see cref="NotFound"/> if
    /// such a flashcard template is not present in the repository
    /// </returns>
    public OneOf<Success, NotFound> Remove(int id);
}