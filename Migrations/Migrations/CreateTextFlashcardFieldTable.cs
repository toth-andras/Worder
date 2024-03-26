// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 1.3.2024

using FluentMigrator;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateTextFlashcardFieldTable, "Creates a table for text flashcard fields")]
public class CreateTextFlashcardFieldTable: Migration
{
    public override void Up()
    {
        if (Schema.Table(TextFlashcardFieldTable.TableName).Exists()) return;

        var query = 
            $@"
                create table {TextFlashcardFieldTable.TableName}
                (
                    {TextFlashcardFieldTable.Id}    serial
                        constraint text_flaschcard_fields_pk
                            primary key,
                    {TextFlashcardFieldTable.FlashcardId}   integer not null
                        constraint text_flaschcard_fields_flashcards_id_fk
                            references {SchemaDescriptor.SchemaName}.{FlashcardTable.TableName} on delete cascade,
                    {TextFlashcardFieldTable.FieldName}   varchar({TextFlashcardFieldTable.MaxNameLength})  not null,
                    {TextFlashcardFieldTable.Value}   varchar({TextFlashcardFieldTable.MaxValueLength}) not null,
                    {TextFlashcardFieldTable.CanBeShownInQuestion}   boolean    not null,
                    {TextFlashcardFieldTable.StyleId}   integer not null
                        constraint text_flaschcard_fields_text_styles_id_fk
                            references {SchemaDescriptor.SchemaName}.{TextStyleTable.TableName} on delete restrict
                );
            ";
        
        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(TextFlashcardFieldTable.TableName).Exists() is false) return;
        
        Delete.Table(TextFlashcardFieldTable.TableName);
    }
}