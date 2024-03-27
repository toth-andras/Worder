// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using Domain.Flashcards;

namespace Migrations.Descriptors;

public static class TagTable
{
    public static readonly string TableName = "tags";
    public static readonly string Id = "id";
    public static readonly string UserId = "user_id";
    public static readonly string TagName = "name";

    public static readonly int MaxTagNameLength = Tag.MaxNameLength;
}