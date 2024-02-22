// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain;

/// <summary>
/// Represents a tag that can be attached to a flashcard.
/// </summary>
public class Tag
{
    /// <summary>
    /// The unique identifier of the tag.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the tag.
    /// </summary>
    public string Name { get; set; }
}