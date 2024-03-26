// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using FluentMigrator;
using Migrations.Descriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateColorTable, "Creates a table for colors")]
public class CreateColorTable: Migration
{
    public override void Up()
    {
        if  (Schema.Table(ColorTable.TableName).Exists()) return;
        
        var query = 
            $@"
                create table {SchemaDescriptor.SchemaName}.{ColorTable.TableName}
                (
                    {ColorTable.Id}   serial
                        constraint colors_pk
                            primary key,
                    {ColorTable.ColorName} varchar({ColorTable.MaxColorNameLength}) unique not null
                );
            ";
            
        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(ColorTable.TableName).Exists() is false) return;

        Delete.Table(ColorTable.TableName);
    }
}