// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 23.2.2024

using FluentValidation;

namespace Domain.Validation;

/// <summary>
/// Encapsulates validation logic for <see cref="Flashcard">Flashcard</see> class.
/// </summary>
public class FlashcardValidator: AbstractValidator<Flashcard>
{
    public FlashcardValidator()
    {
        // If a validator in a rule chain fails, the following validators will not be invoked.
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Term).NotNull().NotEmpty().Length(1, 70);
        RuleFor(x => x.Definition).NotNull().NotEmpty().Length(1, 100);
        When(x => x.Fields is not null && x.Fields.Count > 0, () =>
        {
            RuleForEach(x => x.Fields).SetInheritanceValidator(v =>
            {
                v.Add<TextFlashcardField>(new TextFlashcardFieldValidator());
            });
        });
        When(x => x.Tags is not null && x.Tags.Count > 0, () =>
        {
            RuleForEach(x => x.Tags).SetValidator(new TagValidator());
        });
        RuleFor(x => x.TermStyle).SetValidator(new TextStyleValidator());
        RuleFor(x => x.DefinitionStyle).SetValidator(new TextStyleValidator());
    }
}