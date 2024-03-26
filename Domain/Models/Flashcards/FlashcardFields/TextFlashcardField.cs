// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain.Flashcards;

/// <summary>
/// Represents a flashcard field used to attach additional
/// text data to a flashcard.
/// </summary>
public class TextFlashcardField: FlashcardFieldBase
{
    /// <summary>
    /// The additional text data.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Stores the biggest possible length of the flashcard field value.
    /// </summary>
    public const int MaxValueLength = 100;
}