// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using FluentMigrator;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateUserTable, "Creates a table for users")]
public class CreateUserTable: Migration
{
    public override void Up()
    {
        if (Schema.Table(UserTable.TableName).Exists()) return;
        
        var query =
            $@"
            create extension pgcrypto;
            create table {SchemaDescriptor.SchemaName}.{UserTable.TableName}
            (
                {UserTable.Id}   serial not null
                    constraint users_pk
                        primary key,
                {UserTable.Email}   text    unique   not null,
                {UserTable.Password}   text   not null,
                {UserTable.Salt}    text not null
            );
        ";

        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(UserTable.TableName).Exists() is false) return;
        
        Delete.Table(UserTable.TableName);
    }
}