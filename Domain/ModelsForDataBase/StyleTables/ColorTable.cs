// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

namespace Migrations.Descriptors;

public static class ColorTable
{
    public static readonly string TableName = "colors";
    public static readonly string Id = "id";
    public static readonly string ColorName = "name";
    public static readonly int MaxColorNameLength = 20;
}