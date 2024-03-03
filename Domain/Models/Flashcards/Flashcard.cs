// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain.Flashcards;

/// <summary>
/// Represents a flashcard.
/// </summary>
public class Flashcard
{
    /// <summary>
    /// The unique identifier of the flashcard.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The foreign word stored in the flashcard as a term.
    /// </summary>
    public string Term { get; set; }
    
    /// <summary>
    /// The definition of the term stored in the flashcard.
    /// </summary>
    public string Definition { get; set; }
    
    /// <summary>
    /// Any additional data the user has attached to the
    /// flashcard.
    /// </summary>
    public List<FlashcardFieldBase>? Fields { get; set; }
    
    /// <summary>
    /// All tags the user has attached to the flashcard.
    /// </summary>
    public List<Tag>? Tags { get; set; }
    
    /// <summary>
    /// The style of the flashcard's term.
    /// </summary>
    public TextStyle TermStyle { get; set; }
    
    /// <summary>
    /// The style of the flashcard's definition.
    /// </summary>
    public TextStyle DefinitionStyle { get; set; }

    /// <summary>
    /// Stores the biggest possible length of the term.
    /// </summary>
    public const int MaxTermLength = 70;

    /// <summary>
    /// Stores the biggest possible length of the definition.
    /// </summary>
    public const int MaxDefinitionLength = 100;
}