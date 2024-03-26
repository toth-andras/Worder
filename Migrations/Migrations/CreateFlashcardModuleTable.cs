// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using FluentMigrator;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateFlashcardModuleTable, "Creates a table for flashcard modules")]
public class CreateFlashcardModuleTable: Migration
{
    public override void Up()
    {
        if (Schema.Table(FlashcardModuleTable.TableName).Exists()) return;
        var query =
            $@"
                    create table {SchemaDescriptor.SchemaName}.{FlashcardModuleTable.TableName}
                    (
                        {FlashcardModuleTable.Id}   serial  not null
                            constraint flashcard_modules_pk
                                primary key,
                        {FlashcardModuleTable.UserId}   integer not null
                            constraint flashcard_modules_users_id_fk
                                references {SchemaDescriptor.SchemaName}.{UserTable.TableName} on delete cascade,
                        {FlashcardModuleTable.ModuleName}   varchar({FlashcardModuleTable.MaxModuleNameLength}) not null,
                        {FlashcardModuleTable.Definition} text  not null
                    );
                ";
            
        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(FlashcardModuleTable.TableName).Exists() is false) return;
        
        Delete.Table(FlashcardModuleTable.TableName);
    }
}