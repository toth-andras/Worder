// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain.FlashcardTemplates;

/// <summary>
/// The base class for all flashcard field templates:
/// classes that store the information about a flashcard's field.
/// </summary>
public abstract class FlashcardFieldTemplateBase
{
    /// <summary>
    /// The unique identifier of the flashcard field template.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the flashcard field the template stores
    /// information about.
    /// </summary>
    public int Name { get; }
}