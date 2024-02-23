// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 23.2.2024

using Domain.FlashcardTemplates;
using FluentValidation;

namespace Domain.Validation;

/// <summary>
/// Encapsulates validation logic for
/// <see cref="TextFlashcardFieldTemplate">TextFlashcardFieldTemplate</see> class.
/// </summary>
public class TextFlashcardFieldTemplateValidator: AbstractValidator<TextFlashcardFieldTemplate>
{
    public TextFlashcardFieldTemplateValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 30);
        RuleFor(x => x.Style).NotNull();
    }
}