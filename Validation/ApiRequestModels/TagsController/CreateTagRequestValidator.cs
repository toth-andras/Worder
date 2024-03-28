// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using ApiRequestModels;
using Domain.Flashcards;
using FluentValidation;

namespace Validation.ApiRequestModels.TagsController;

public class CreateTagRequestValidator : AbstractValidator<CreateTagRequest>
{
    public CreateTagRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x).NotNull();

        When(x => x is not null, () =>
        {
            RuleFor(x => x.TagName).NotNull().Length(1, Tag.MaxNameLength);
        });
    }
}