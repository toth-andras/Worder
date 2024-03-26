// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using FluentMigrator;
using Migrations.Descriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateSchema, "Adds the schema for all the tables")]
public class CreateSchema: Migration
{
    public override void Up()
    {
        if (Schema.Schema(SchemaDescriptor.SchemaName).Exists()) return;
        
        Create.Schema(SchemaDescriptor.SchemaName);
    }

    public override void Down()
    {
        if (Schema.Schema(SchemaDescriptor.SchemaName).Exists() is false) return;
        
        Delete.Schema(SchemaDescriptor.SchemaName);
    }
}