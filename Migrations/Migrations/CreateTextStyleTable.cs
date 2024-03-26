// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

using FluentMigrator;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateTextStyleTable, "Creates a table for text styles")]
public class CreateTextStyleTable: Migration
{
    public override void Up()
    {
        if (Schema.Table(TextStyleTable.TableName).Exists()) return;
        
        var query =
            $@"
                create table {SchemaDescriptor.SchemaName}.{TextStyleTable.TableName}
                (
                    {TextStyleTable.Id}   serial    not null
                        constraint text_styles_pk
                            primary key,
                    {TextStyleTable.TextIsRightToLeft}   boolean    not null,
                    {TextStyleTable.FontId}   integer   not null
                        constraint text_styles_fonts_id_fk
                            references {SchemaDescriptor.SchemaName}.{FontTable.TableName} on delete restrict,
                    {TextStyleTable.FontSize}   smallint    not null,
                    {TextStyleTable.ColorId}    integer not null
                        constraint text_styles_colors_id_fk
                            references {SchemaDescriptor.SchemaName}.{ColorTable.TableName} on delete restrict
                );
            ";
            
        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(TextStyleTable.TableName).Exists() is false) return;
        
        Delete.Table(TextStyleTable.TableName);
    }
}