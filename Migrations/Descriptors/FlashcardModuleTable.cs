// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using Domain.Flashcards;

namespace Migrations.Descriptors;

public static class FlashcardModuleTable
{
    public static readonly string TableName = "flashcard_modules";
    public static readonly string Id = "id";
    public static readonly string UserId = "user_id";
    public static readonly string ModuleName = "name";
    public static readonly string Definition = "definition";

    public static readonly int MaxModuleNameLength = FlashcardModule.MaxNameLength;
}