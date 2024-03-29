// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using ApiRequestModels;
using FluentValidation;

namespace Validation.ApiRequestModels.UsersController;

public class VerifyTokenRequestValidator : AbstractValidator<VerifyTokenRequest>
{
    public VerifyTokenRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x).NotNull();

        When(x => x is not null, () =>
        {
            RuleFor(x => x.Token).NotNull().NotEmpty();
        });
    }
}