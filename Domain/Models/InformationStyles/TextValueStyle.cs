// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 22.2.2024

namespace Domain;

/// <summary>
/// Represents a style: a class that stores the information about
/// presentation of a text piece to the user.
/// </summary>
public class TextValueStyle
{
    /// <summary>
    /// The unique identifier of the style.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the font that must be used in text piece
    /// presentation.
    /// </summary>
    public string Font { get; set; }
    
    /// <summary>
    /// Tha size of the font that must be used in text piece
    /// presentation.
    /// </summary>
    public short FontSize { get; set; }
    
    /// <summary>
    /// Indicates whether the text piece must be presented in
    /// right-to-left direction.
    /// <remarks>This field is used to present correctly text
    /// pieces written in languages that have right-to-left
    /// writing systems.</remarks>
    /// </summary>
    public bool TextIsRightToLeft { get; set; }
    
    /// <summary>
    /// The name of the color that must be used in text piece
    /// presentation.
    /// </summary>
    public string Color { get; set; }
}