// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using FluentMigrator;
using Migrations.Descriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateFontTable, "Creates a table for fonts")]
public class CreateFontTable: Migration
{
    public override void Up()
    {
        if (Schema.Table(FontTable.TableName).Exists()) return;
        
        var query = 
            $@"
                create table {SchemaDescriptor.SchemaName}.{FontTable.TableName}
                (
                    {FontTable.Id}   serial
                        constraint fonts_pk
                            primary key,
                    {FontTable.FontName} varchar({FontTable.MaxFontNameLength}) unique not null
                );
            ";

        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(FontTable.TableName).Exists() is false) return;
        
        Delete.Table(FontTable.TableName);
    }
}