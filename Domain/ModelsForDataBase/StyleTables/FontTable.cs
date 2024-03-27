// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

namespace Migrations.Descriptors;

public static class FontTable
{
    public static readonly string TableName = "fonts";
    public static readonly string Id = "id";
    public static readonly string FontName = "name";
    public static readonly int MaxFontNameLength = 20;
}