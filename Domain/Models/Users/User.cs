// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 3.3.2024

namespace Domain.Users;

/// <summary>
/// Represents the user.
/// </summary>
public class User
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// User's email.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// User's password.
    /// </summary>
    public string PasswordHash { get; set; }
}