// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using Domain.Flashcards;

namespace ApiRequestModels;

public record CreateTagRequest(int UserId, string TagName);

public record GetUserTagsRequest(int UserId);
public record UpdateTagRequest(int UserId, int TagId, Tag NewValue);
public record DeleteTagRequest(int  TagId);