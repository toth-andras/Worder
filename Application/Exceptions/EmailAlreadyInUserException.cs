// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 25.3.2024

namespace Application.Exceptions;

public class EmailAlreadyInUserException: ApplicationException
{
    public EmailAlreadyInUserException() { }
    
    public EmailAlreadyInUserException(string message) : base(message) { }
}