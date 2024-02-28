// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using Application.ReturnTypes;
using Domain;
using OneOf;

namespace Application.InformationStyles;

/// <summary>
/// Represents a common interface for all text style repositories.
/// </summary>
public interface ITextStyleRepository
{
    /// <summary>
    /// Gets the text style.
    /// </summary>
    /// <param name="id">text style identifier</param>
    /// <returns>
    /// The text style or <see cref="NotFound"/>,
    /// such a text style is not present in the repository
    /// </returns>
    public OneOf<TextStyle, NotFound> GetById(int id);

    /// <summary>
    /// Adds the text style.
    /// </summary>
    /// <param name="textStyle">text style identifier</param>
    /// <returns>
    /// <see cref="Success"/> if the addition was executed successfully or <see cref="ValidationFailed"/>
    /// if the given text style did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Add(TextStyle textStyle);

    /// <summary>
    /// Updates the data that represents the text style to the provided state.
    /// </summary>
    /// <param name="textStyle">the text style with new state</param>
    /// <returns>
    /// <see cref="Success"/> if the update was executed successfully or <see cref="ValidationFailed"/>
    /// if the new state of the text style did not pass validation
    /// </returns>
    public OneOf<Success, ValidationFailed> Update(TextStyle textStyle);
    
    /// <summary>
    /// Removes the text style.
    /// </summary>
    /// <param name="id">text style identifier</param>
    /// <returns>
    /// <see cref="Success"/> if the removal was executed successfully or <see cref="NotFound"/>,
    /// such a text style is not present in the repository
    /// </returns>
    public OneOf<Success, NotFound> Remove(int id);
}