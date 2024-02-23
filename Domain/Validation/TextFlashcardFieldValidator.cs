// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 23.2.2024

using FluentValidation;

namespace Domain.Validation;

/// <summary>
/// Encapsulates validation logic for <see cref="TextFlashcardField">TextFlashcardField</see> class.
/// </summary>
public class TextFlashcardFieldValidator: AbstractValidator<TextFlashcardField>
{
    public TextFlashcardFieldValidator()
    {
        // If a validator in a rule chain fails, the following validators will not be invoked.
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 30);
        RuleFor(x => x.Value).NotNull().NotEmpty().Length(1, 100);
        RuleFor(x => x.Style).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Style).SetValidator(new TextStyleValidator());
        });
    }
}