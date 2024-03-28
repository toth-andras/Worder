// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using Domain.Flashcards;

namespace Migrations.TableDescriptors;

public static class FlashcardTable
{
    public static readonly string TableName = "flashcards";
    public static readonly string Id = "id";
    public static readonly string UserId = "user_id";
    public static readonly string Term = "term";
    public static readonly string Definition = "definition";

    public static readonly int MaxTermLength = Flashcard.MaxTermLength;
    public static readonly int MaxDefinitionLength = Flashcard.MaxDefinitionLength;
}