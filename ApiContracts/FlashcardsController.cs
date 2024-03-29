// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using Domain.Flashcards;

namespace ApiRequestModels;

public record CreateFlaschardRequest(
    int UserId,
    string Term,
    string Definition,
    IEnumerable<FlashcardFieldBase>? Fields,
    IEnumerable<Tag>? Tags);
public record UpdateFlashcardRequest(int FlashcardId, Flashcard NewValue);