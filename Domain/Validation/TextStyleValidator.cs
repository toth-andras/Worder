// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 23.2.2024

using FluentValidation;

namespace Domain.Validation;

/// <summary>
/// Encapsulates validation logic for <see cref="TextStyle">TextStyle</see> class.
/// </summary>
public class TextStyleValidator: AbstractValidator<TextStyle>
{
    public TextStyleValidator()
    {
        // If a validator in a rule chain fails, the following validators will not be invoked.
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Font).NotNull().NotEmpty();
        RuleFor(x => x.FontSize).GreaterThan((short)0);
        RuleFor(x => x.Color).NotNull().NotEmpty();
    }
}