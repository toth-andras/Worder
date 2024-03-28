// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

namespace Application.Exceptions;

public class TagNameCollisionException : ApplicationException
{
    public TagNameCollisionException() { }
    
    public TagNameCollisionException(string message) : base(message) { }
}