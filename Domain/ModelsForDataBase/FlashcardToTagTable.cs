// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

namespace Domain.ModelsForDataBase;

public static class FlashcardToTagTable
{
    public static string TableName = "flashcards_tags";
    public static string Id = "id";
    public static string FlashcardId = "flashcard_id";
    public static string TagId = "tag_id";
}