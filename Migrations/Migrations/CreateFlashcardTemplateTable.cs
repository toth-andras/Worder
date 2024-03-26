// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 1.3.2024

using FluentMigrator;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateFlashcardTemplateTable, "Creates a table for flashcard templates")]
public class CreateFlashcardTemplateTable: Migration
{
    public override void Up()
    {
        if (Schema.Table(FlashcardTemplateTable.TableTame).Exists()) return;

        var query =
            $@"
                create table {SchemaDescriptor.SchemaName}.{FlashcardTemplateTable.TableTame}
                (
                    {FlashcardTemplateTable.Id}   serial  not null
                        constraint flashcard_templates_pk
                            primary key,
                    {FlashcardTemplateTable.TemplateName}   varchar({FlashcardTemplateTable.MaxNameLength}) not null,
                    {FlashcardTemplateTable.UserId}   integer not null
                        constraint flashcard_templates_users_id_fk
                            references {SchemaDescriptor.SchemaName}.{UserTable.TableName} on delete cascade
                );
            ";
        
        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(FlashcardTemplateTable.TableTame).Exists() is false) return;
       
        Delete.Table(FlashcardTemplateTable.TableTame);
    }
}