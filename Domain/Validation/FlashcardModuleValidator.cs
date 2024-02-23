// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 23.2.2024

using FluentValidation;

namespace Domain.Validation;

/// <summary>
/// Encapsulates validation logic for <see cref="FlashcardModule">FlashcardModule</see> class.
/// </summary>
public class FlashcardModuleValidator: AbstractValidator<FlashcardModule>
{
    public FlashcardModuleValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 30);
        RuleFor(x => x.Definition).NotNull();
        RuleFor(x => x.Flashcards)
            .NotNull()
            .Must(x => x.Count > 0)
            .When(x => x.Flashcards is not null, ApplyConditionTo.CurrentValidator);
        
        RuleForEach(x => x.Flashcards).SetValidator(new FlashcardValidator())
            .When(x => x.Flashcards is not null && x.Flashcards.Count > 0);
    }
}