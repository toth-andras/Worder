// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain.FlashcardTemplates;

/// <summary>
/// Rerpesents a flashcard field template that stores the
/// information about a flashcard text field.
/// </summary>
public class TextFlashcardFieldTemplate
{
    /// <summary>
    /// Stores the style of the flashcard text field.
    /// </summary>
    public TextStyle Style { get; set; }
}