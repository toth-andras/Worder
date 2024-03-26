// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using FluentMigrator;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateTagTable, "Creates a table for tags")]
public class CreateTagTable: Migration
{
    public override void Up()
    {
        if (Schema.Table(TagTable.TableName).Exists()) return;

        var query =
            $@"
                create table {SchemaDescriptor.SchemaName}.{TagTable.TableName}
                (
                    {TagTable.Id}   serial  not null
                        constraint tags_pk
                            primary key,
                    {TagTable.UserId}   integer not null
                        constraint tags_users_id_fk
                            references {SchemaDescriptor.SchemaName}.{UserTable.TableName} on delete cascade,
                    {TagTable.TagName}   varchar({TagTable.MaxTagNameLength}) not null
                );
            ";
        
        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(TagTable.TableName).Exists() is false) return;
        
        Delete.Table(TagTable.TableName);
    }
}