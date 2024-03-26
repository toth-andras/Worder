// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 25.3.2024

namespace Application.Exceptions;

public class NotFoundException: ApplicationException
{
    public NotFoundException() { }
    
    public NotFoundException(string message) : base(message) { }
}