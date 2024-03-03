// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain.Flashcards;

/// <summary>
/// The base class for all classes that represent flashcard fields — an data
/// pieces the user can add to the flashcard.
/// </summary>
public abstract class FlashcardFieldBase
{
    /// <summary>
    /// The unique identifier of the flashcard field.
    /// </summary>
    public int Id { get; set; }    
    
    /// <summary>
    /// The name of the flashcard field.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Shows whether the flashcard field can be added to flashcard presentation
    /// in study mode.
    /// <remarks>In other words, this property shows whether the flashcard
    /// field contains any information that might let the user guess the flashcard
    /// term or definition instead of recalling it, thus the flashcard field
    /// must be hidden from the user when they study the flashcard.</remarks>
    /// </summary>
    public bool CanBeShownInQuestion { get; set; }

    /// <summary>
    /// Stores the biggest possible length of flashcard field name.
    /// </summary>
    public const int MaxNameLength = 30;
}