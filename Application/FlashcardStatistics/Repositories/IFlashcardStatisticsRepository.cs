// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using OneOf;
using Application.ReturnTypes;

namespace Application.Repositories;

/// <summary>
/// Represents a common interface for all flashcard statistics repositories.
/// </summary>
public interface IFlashcardStatisticsRepository
{
    /// <summary>
    /// Gets the flashcard statistics.
    /// </summary>
    /// <param name="id">flashcard statistics identifier</param>
    /// <returns>
    /// The flashcard statistics or <see cref="NotFound"/>,
    /// such a flashcard statistics is not present in the repository
    /// </returns>
    public OneOf<Domain.FlashcardStatistics, NotFound> GetById(int id);

    /// <summary>
    /// Returns the flashcard statistics for the flashcard.
    /// </summary>
    /// <param name="flashcardId">flashcard identifier</param>
    /// <returns>
    /// The flashcard statistics for the given flashcard or <see cref="NotFound"/>,
    /// if such a flashcard statistics is not present in the repository
    /// </returns>
    public OneOf<Domain.FlashcardStatistics, NotFound> GetByFlashcardId(int flashcardId);

    /// <summary>
    /// Adds the flashcard statistics.
    /// </summary>
    /// <param name="flashcardId">Flashcard identifier</param>
    /// <param name="statistics">The flashcard statistics</param>
    /// <returns>
    /// <see cref="Success"/> if the addition was executed successfully or <see cref="ValidationFailed"/>
    /// if the given flashcards statistics did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Add(int flashcardId, Domain.FlashcardStatistics statistics);
    
    /// <summary>
    /// Updates the data that represents the flashcard statistics to the provided state.
    /// </summary>
    /// <param name="statistics">the flashcard statistics with new state</param>
    /// <returns>
    /// <see cref="Success"/> if the update was executed successfully or <see cref="ValidationFailed"/>
    /// if the new state did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Update(Domain.FlashcardStatistics statistics);

    /// <summary>
    /// Removes the flashcard statistics.
    /// </summary>
    /// <param name="id">flashcard statistics identifier</param>
    /// <returns>
    /// <see cref="Success"/> if the removal was executed successfully and <see cref="NotFound"/>,
    /// if such a flashcard statistics is not present in the repository
    /// </returns>
    public OneOf<Success, NotFound> Remove(int id);
}