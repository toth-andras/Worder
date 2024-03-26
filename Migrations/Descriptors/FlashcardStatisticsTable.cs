// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 1.3.2024

namespace Migrations.Descriptors;

public static class FlashcardStatisticsTable
{
    public static readonly string TableName = "flashcard_statistics";
    public static readonly string Id = "id";
    public static readonly string FlashcardId = "flashcard_id";
    public static readonly string LastTimeRevised = "last_time_revised";
    public static readonly string LastAnswerCorrect = "last_answer_correct";
    public static readonly string FlashcardBox = "flashcard_box";
}