// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain;

/// <summary>
/// Represents a module: a special class that stores flashcards.
/// </summary>
public class FlashcardModule
{
    /// <summary>
    /// The unique identifier of the module.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the module.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The definition that stores some additional information
    /// about the module.
    /// </summary>
    public string Definition { get; set; }

    /// <summary>
    /// Flashcards that are attached to the module.
    /// </summary>
    public List<Flashcard> Flashcards { get; set; }

    /// <summary>
    /// Represents the biggest possible length of the flashcard module name.
    /// </summary>
    public const int MaxNameLength = 30;
}