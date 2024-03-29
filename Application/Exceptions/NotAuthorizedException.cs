// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

namespace Application.Exceptions;

public class NotAuthorizedException : ApplicationException
{
    public NotAuthorizedException() { }
    
    public NotAuthorizedException(string message) : base(message) { }
}