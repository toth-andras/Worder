// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain.Flashcards;

/// <summary>
/// Represents a class that stores a flashcard's statistics.
/// </summary>
public class FlashcardStatistics
{
    /// <summary>
    /// The unique identifier of the statistics.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The unique identifier of the flashcard the statistics
    /// is stored for.
    /// </summary>
    public int FlashcardId { get; set; }
    
    /// <summary>
    /// The date and time the flashcard has been revised for
    /// the last time.
    /// </summary>
    public DateTime? LastTimeRevisedUtc { get; set; } 
    
    /// <summary>
    /// Indicates whether the user answered correctly the
    /// last time he has revised the flashcard.
    /// </summary>
    public bool? LastAnswerCorrect { get; set; }
    
    /// <summary>
    /// The interval learning box containing the flashcard.
    /// </summary>
    public int FlashCardBox { get; set; }

    public const int MinFlashCardBox = 0;
    public const int MaxFlashCardBox = 3;
}