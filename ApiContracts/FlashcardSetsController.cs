// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

namespace ApiRequestModels;

public record FlashcardSetRequest(int UserId, int FlashcardsAmount);