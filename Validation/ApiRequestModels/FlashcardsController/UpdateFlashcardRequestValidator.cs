// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using ApiRequestModels;
using Domain.Validation;
using FluentValidation;

namespace Validation.ApiRequestModels.FlashcardsController;

public class UpdateFlashcardRequestValidator : AbstractValidator<UpdateFlashcardRequest>
{
    public UpdateFlashcardRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x).NotNull();
        When(x => x is not null, () =>
        {
            RuleFor(x => x.NewValue).NotNull().SetValidator(new FlashcardValidator());
        });
    }
}