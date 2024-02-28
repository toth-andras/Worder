// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

namespace Application.ReturnTypes;

/// <summary>
/// A type which is returned whenever an operation has been executed successfully.
/// </summary>
public class Success
{
    public static Success Empty { get; } = new Success();
}