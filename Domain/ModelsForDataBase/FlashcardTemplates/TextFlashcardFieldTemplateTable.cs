// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 1.3.2024

using Domain.Flashcards;

namespace Migrations.Descriptors;

public static class TextFlashcardFieldTemplateTable
{
    public static readonly string TableName = "text_flashcard_field_templates";
    public static readonly string Id = "id";
    public static readonly string TemplateId = "template_id";
    public static readonly string FieldTemplateName = "name";
    public static readonly string StyleId = "style_id";

    public static readonly int MaxFieldTemplateNameLength = FlashcardFieldBase.MaxNameLength;
}