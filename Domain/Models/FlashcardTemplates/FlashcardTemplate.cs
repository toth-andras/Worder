// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain.FlashcardTemplates;

/// <summary>
/// Represents a flashcard template: a class that contains information
/// about a flashcard's fields that can be used to create other flashcards. 
/// </summary>
public class FlashcardTemplate
{
    /// <summary>
    /// The unique identifier of the template.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the template.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The information about the flashcard's fields.
    /// </summary>
    public List<FlashcardFieldTemplateBase> FieldTemplates { get; set; }

    /// <summary>
    /// Stores the biggest possible length of the name.
    /// </summary>
    public const int MaxNameLength = 30;
}