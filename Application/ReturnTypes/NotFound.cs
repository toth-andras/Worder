// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

namespace Application.ReturnTypes;

/// <summary>
/// A type that is return whenever something was not found.
/// </summary>
public class NotFound
{
    public static NotFound Empty { get; } = new NotFound();
}