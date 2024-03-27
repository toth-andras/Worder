// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using FluentMigrator;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateFlashcardTable, "Creates a table for flashcards")]
public class CreateFlashcardTable: Migration
{
    public override void Up()
    {
        if (Schema.Table(FlashcardTable.TableName).Exists()) return;
        
        var query =
            $@"
                create table {SchemaDescriptor.SchemaName}.{FlashcardTable.TableName}
                (
                    {FlashcardTable.Id}   serial  not null
                        constraint flashcards_pk
                            primary key,
                    {FlashcardTable.UserId}   integer not null
                        constraint flashcards_users_id_fk
                            references {SchemaDescriptor.SchemaName}.{UserTable.TableName} on delete cascade,
                    {FlashcardTable.Term}   varchar({FlashcardTable.MaxTermLength})  not null,
                    {FlashcardTable.Definition}   varchar({FlashcardTable.MaxDefinitionLength}) not null
                );
            ";

        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(FlashcardTable.TableName).Exists() is false) return;
        
        Delete.Table(FlashcardTable.TableName);
    }
}