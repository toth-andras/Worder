// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.2.2024

using FluentValidation.Results;

namespace Application.ReturnTypes;

/// <summary>
/// A type that is returned whenever a validation failure has occured.
/// </summary>
public record ValidationFailed(ValidationResult ValidationResult);