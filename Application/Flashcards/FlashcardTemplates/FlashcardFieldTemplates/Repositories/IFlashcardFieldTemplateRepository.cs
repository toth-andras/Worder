// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using Application.ReturnTypes;
using Domain.FlashcardTemplates;
using OneOf;

namespace Application.Repositories;

/// <summary>
/// Represents a common interface for all flashcard
/// field template repositories.
/// </summary>
public interface IFlashcardFieldTemplateRepository<T> where T: FlashcardFieldTemplateBase
{
    /// <summary>
    /// Gets the flashcard field template.
    /// </summary>
    /// <param name="id">flashcard field template identifier</param>
    /// <returns>
    /// The flashcard field template or <see cref="NotFound"/>,
    /// such a flashcard field template is not present in the repository
    /// </returns>
    public OneOf<T, NotFound> GetById(int id);

    /// <summary>
    /// Gets all flashcard templates that are attached to the template.
    /// </summary>
    /// <param name="flashcardTemplateId">template identifier</param>
    /// <returns>The collection of flashcard field templates</returns>
    public IEnumerable<T> GetByFlashcardTemplateId(int flashcardTemplateId);

    /// <summary>
    /// Adds the flashcard field template.
    /// </summary>
    /// <param name="flashcardTemplateId">flashcard template id</param>
    /// <param name="fieldTemplate">flashcard field template</param>
    /// <returns>
    /// <see cref="Success"/> if the addition was executed successfully or <see cref="ValidationFailed"/>
    /// if the given flashcard field template did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Add(int flashcardTemplateId, T fieldTemplate);

    /// <summary>
    /// Updates the data that represents the flashcard to the provided state.
    /// </summary>
    /// <param name="fieldTemplate">the flashcard field template with new state</param>
    /// <returns>
    /// <see cref="Success"/> if the update was executed successfully or <see cref="ValidationFailed"/>
    /// if the new state of the flashcard field template did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Update(T fieldTemplate);
    
    /// <summary>
    /// Removes the flashcard field template.
    /// </summary>
    /// <param name="id">flashcard field template identifier</param>
    /// <returns>
    /// <see cref="Success"/> if the removal was executed successfully or <see cref="NotFound"/>,
    /// such a flashcard field template is not present in the repository
    /// </returns>
    public OneOf<Success, ValidationFailed> Remove(int id);
}