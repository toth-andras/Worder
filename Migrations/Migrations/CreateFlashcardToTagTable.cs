// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using Domain.ModelsForDataBase;
using FluentMigrator;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateFlashcardToTagTable)]
public class CreateFlashcardToTagTable : Migration
{
    public override void Up()
    {
        if (Schema.Table(FlashcardToTagTable.TableName).Exists()) return;
        
        var query =
            @$"
                create table {SchemaDescriptor.SchemaName}.{FlashcardToTagTable.TableName}
                (
                    {FlashcardToTagTable.Id}    serial  not null
                        constraint flashcards_tags_pk
                            primary key,
                    {FlashcardToTagTable.FlashcardId}   integer not null
                        constraint flashcards_tags_flashcards_id_fk
                            references {SchemaDescriptor.SchemaName}.{FlashcardTable.TableName} on delete cascade,
                    {FlashcardToTagTable.TagId} integer not null
                        constraint flashcards_tags_tags_id_fk
                            references {SchemaDescriptor.SchemaName}.{TagTable.TableName} on delete cascade
                );
            ";
        
        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(FlashcardToTagTable.TableName).Exists() is false) return;
        
        Delete.Table(FlashcardToTagTable.TableName);
    }
}