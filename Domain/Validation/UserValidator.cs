// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 3.3.2024

using FluentValidation;

namespace Domain.Validation;

using Users;

/// <summary>
/// Encapsulates validation logic for <see cref="User">TextStyle</see> class.
/// </summary>
public class UserValidator: AbstractValidator<User>
{
    public UserValidator()
    {
        // If a validator in a rule chain fails, the following validators will not be invoked.
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.PasswordHash).NotNull().NotEmpty();
    }
}