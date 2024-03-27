// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using ApiRequestModels;
using FluentValidation;

namespace Validation.ApiRequestModels.UsersController;

public class EmailRequestValidator : AbstractValidator<EmailRequest>
{
    public EmailRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x).NotNull();
        When(x => x is not null, () =>
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        });
    }
}