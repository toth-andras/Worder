// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using ApiRequestModels;
using Domain.Flashcards;
using Domain.Validation;
using FluentValidation;

namespace Validation.ApiRequestModels.FlashcardsController;

public class CreateFlashcardRequestValidator : AbstractValidator<CreateFlaschardRequest>
{
    public CreateFlashcardRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x).NotNull();

        When(x => x is not null, () =>
        {
            RuleFor(x => x.Term).NotNull().Length(1, Flashcard.MaxTermLength);
            RuleFor(x => x.Definition).NotNull().Length(1, Flashcard.MaxDefinitionLength);

            When(x => x.Fields is not null, () =>
            {
                RuleForEach(x => x.Fields).SetInheritanceValidator(v =>
                {
                    v.Add<TextFlashcardField>(new TextFlashcardFieldValidator());
                });
            });
        });
    }
}