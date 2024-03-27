// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using ApiRequestModels;
using FluentValidation;

namespace Validation.ApiRequestModels.UsersController;

public class EmailPasswordRequestValidator : AbstractValidator<EmailPasswordRequest>
{
    public EmailPasswordRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x).NotNull();

        When(x => x is not null, () =>
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().Length(6, 1000);
        });
    }
}