// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using ApiRequestModels;
using Domain.Validation;
using FluentValidation;

namespace Validation.ApiRequestModels.TagsController;

public class UpdateTagRequestValidator : AbstractValidator<UpdateTagRequest>
{
    public UpdateTagRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x).NotNull();

        When(x => x is not null, () =>
        {
            RuleFor(x => x.NewValue).NotNull().SetValidator(new TagValidator());
        });
    }
}