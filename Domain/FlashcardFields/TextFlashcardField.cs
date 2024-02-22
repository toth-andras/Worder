// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain;

/// <summary>
/// Represents a flashcard field used to attach addititonal
/// text data to a flashcard.
/// </summary>
public class TextFlashcardField: FlashcardFieldBase
{
    /// <summary>
    /// The additional text data.
    /// </summary>
    public string Value { get; set; }
    
    /// <summary>
    /// The style of the additional text data.
    /// </summary>
    public TextValueStyle Style {get; set; }
}