// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 1.3.2024

using FluentMigrator;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateTextFlashcardFieldTemplateTable, "Creates a table for text flashcard field templates")]
public class CreateTextFlashcardFieldTemplateTable: Migration
{
    public override void Up()
    {
        if (Schema.Table(TextFlashcardFieldTemplateTable.TableName).Exists()) return;

        var query = 
            $@"
                create table {TextFlashcardFieldTemplateTable.TableName}
                (
                    {TextFlashcardFieldTemplateTable.Id}   serial
                        constraint text_flashcard_field_templates_pk
                            primary key,
                    {TextFlashcardFieldTemplateTable.FieldTemplateName}  varchar({TextFlashcardFieldTemplateTable.MaxFieldTemplateNameLength})  not null,
                    {TextFlashcardFieldTemplateTable.TemplateId}   integer  not null
                        constraint text_flashcard_field_templates_flashcard_templates_id_fk
                            references {SchemaDescriptor.SchemaName}.{FlashcardTemplateTable.TableTame} on delete cascade,
                    {TextFlashcardFieldTemplateTable.StyleId}   integer not null
                        constraint text_flashcard_field_templates_text_styles_id_fk
                            references {SchemaDescriptor.SchemaName}.{TextStyleTable.TableName} on delete restrict
                );
            ";
        
        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(TextFlashcardFieldTemplateTable.TableName).Exists() is false) return;
       
        Delete.Table(TextFlashcardFieldTemplateTable.TableName);
    }
}