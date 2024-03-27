// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using Domain.Flashcards;

namespace Migrations.Descriptors;

public static class TextFlashcardFieldTable
{
    public static readonly string TableName = "text_flashcard_fields";
    public static readonly string Id = "id";
    public static readonly string FlashcardId = "flashcard_id";
    public static readonly string FieldName = "name";
    public static readonly string Value = "value";
    public static readonly string CanBeShownInQuestion = "can_be_shown_in_question";

    public static readonly int MaxNameLength = FlashcardFieldBase.MaxNameLength;
    public static readonly int MaxValueLength = TextFlashcardField.MaxValueLength;
}