// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

namespace Migrations;

/// <summary>
/// Represents the order according to which migrations are executed.
/// </summary>
public enum MigrationVersions: long
{
    CreateSchema = 0,
    CreateUserTable = 10,
    CreateFlashcardModuleTable = 20,
    CreateFontTable = 30,
    CreateColorTable = 40,
    CreateTextStyleTable = 50,
    CreateFlashcardTable = 60,
    CreateTextFlashcardFieldTable = 70,
    CreateTagTable = 80,
    CreateFlashcardStatisticsTable = 90,
    CreateFlashcardTemplateTable = 100,
    CreateTextFlashcardFieldTemplateTable = 110
}