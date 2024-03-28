// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 1.3.2024

using FluentMigrator;
using Migrations.Descriptors;
using Migrations.TableDescriptors;

namespace Migrations;

[Migration((long)MigrationVersions.CreateFlashcardStatisticsTable, "Creates a table for flashcard statistics")]
public class CreateFlashcardStatisticsTable: Migration
{
    public override void Up()
    {
        if (Schema.Table(FlashcardStatisticsTable.TableName).Exists()) return;

        var query = 
            $@"
                create table {SchemaDescriptor.SchemaName}.{FlashcardStatisticsTable.TableName}
                (
                    {FlashcardStatisticsTable.Id}   serial  not null
                        constraint flashcard_statistics_pk
                            primary key,
                    {FlashcardStatisticsTable.FlashcardId}   integer not null
                        constraint flashcard_statistics_flashcards_id_fk
                            references {SchemaDescriptor.SchemaName}.{FlashcardTable.TableName} on delete cascade,
                    {FlashcardStatisticsTable.LastTimeRevisedUtc}   timestamp default null,
                    {FlashcardStatisticsTable.LastAnswerCorrect}   boolean default null,
                    {FlashcardStatisticsTable.FlashcardBox}   smallint not null default 0
                );
            ";
        
        Execute.Sql(query);
    }

    public override void Down()
    {
        if (Schema.Table(FlashcardStatisticsTable.TableName).Exists() is false) return;
        
        Delete.Table(FlashcardStatisticsTable.TableName);
    }
}