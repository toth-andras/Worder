// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 1.3.2024

using Domain.FlashcardTemplates;

namespace Migrations.Descriptors;

public static class FlashcardTemplateTable
{
    public static readonly string TableTame = "flashcard_templates";
    public static readonly string Id = "id";
    public static readonly string UserId = "user_id";
    public static readonly string TemplateName = "name";

    public static readonly int MaxNameLength = FlashcardTemplate.MaxNameLength;
}