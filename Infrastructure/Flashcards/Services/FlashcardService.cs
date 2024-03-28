// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 26.3.2024

using System.Data;
using Application.Flashcards.FlashcardFields.Repositories;
using Application.Flashcards.Repositories;
using Application.Flashcards.Services;
using Application.Repositories;
using Domain.Flashcards;

namespace Infrastructure.Flashcards.Services;

public class FlashcardService : IFlashcardService
{
    private readonly IDbConnection _dbConnection;
    private readonly IFlashcardCoreRepository _flashcardCoreRepository;
    private readonly ITextFlashcardFieldRepository _textFieldRepository;
    private readonly IFlashcardStatisticsRepository _statisticsRepository;
    private readonly IFlashcardTagRelationRepository _tagRelationRepository;
    private readonly ITagRepository _tagRepository;

    public FlashcardService(
        IDbConnection connection,
        IFlashcardCoreRepository flashcardCoreRepository,
        ITextFlashcardFieldRepository textFieldRepository,
        IFlashcardStatisticsRepository statisticsRepository,
        IFlashcardTagRelationRepository relationRepository,
        ITagRepository tagRepository)
    {
        _dbConnection = connection;
        _flashcardCoreRepository = flashcardCoreRepository;
        _textFieldRepository = textFieldRepository;
        _statisticsRepository = statisticsRepository;
        _tagRelationRepository = relationRepository;
        _tagRepository = tagRepository;
    }

    public async Task<Flashcard> CreateFlashcard(
        int userId,
        string term,
        string definition,
        IEnumerable<FlashcardFieldBase>? fields,
        IEnumerable<Tag>? tags)
    {
        using var transaction = _dbConnection.BeginTransaction();
        Flashcard flashcard;

        try
        {
            flashcard = await _flashcardCoreRepository.Create(userId, term, definition, _dbConnection, transaction);
            if (fields is not null)
            {
                flashcard.Fields = new List<FlashcardFieldBase>();
                foreach (var field in fields.OfType<TextFlashcardField>())
                {
                    flashcard.Fields.Add(await _textFieldRepository.Create(flashcard.Id, field.Name,
                        field.CanBeShownInQuestion, field.Value, _dbConnection, transaction));
                }
            }
            if (tags is not null)
            {
                flashcard.Tags = new List<Tag>();
                foreach (var tag in tags)
                {
                    if (tag.Id is not null)
                    {
                        await _tagRelationRepository.CreateRelation(flashcard.Id, tag.Id.Value, _dbConnection, transaction);
                        flashcard.Tags.Add(tag);
                        continue;
                    }

                    var newTag = await _tagRepository.Create(userId, tag.Name, _dbConnection, transaction);
                    await _tagRelationRepository.CreateRelation(flashcard.Id, newTag.Id!.Value, _dbConnection,
                        transaction);
                    flashcard.Tags.Add(newTag);
                }
            }

            await _statisticsRepository.Create(flashcard.Id, _dbConnection, transaction);

            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }

        return flashcard;
    }

    public async Task<Flashcard> GetFlashcard(int id)
    {
        using var transaction = _dbConnection.BeginTransaction();
        try
        {
            var flashcard = await _flashcardCoreRepository.GetById(id, _dbConnection, transaction);

            flashcard.Fields = new List<FlashcardFieldBase>();
            flashcard.Fields.AddRange(
                await _textFieldRepository.GetFlashcardTextFields(id, _dbConnection, transaction));
            if (flashcard.Fields.Count == 0)
            {
                flashcard.Fields = null;
            }

            flashcard.Tags = new List<Tag>();
            flashcard.Tags.AddRange(await _tagRelationRepository.GetFlashcardTags(flashcard.Id, _dbConnection, transaction));
            if (flashcard.Tags.Count == 0)
            {
                flashcard.Tags = null;
            }

            transaction.Commit();
            return flashcard;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<IEnumerable<Flashcard>> GetUserFlashcards(int userId)
    {
        using var transaction = _dbConnection.BeginTransaction();

        try
        {
            var flashcardCores = (await _flashcardCoreRepository.GetByUserId(userId, _dbConnection, transaction)).ToList();
            foreach (var flashcard in flashcardCores)
            {
                var textFields = await _textFieldRepository.GetFlashcardTextFields(flashcard.Id, _dbConnection, transaction);
                var textFlashcardFields = textFields as TextFlashcardField[] ?? textFields.ToArray();
                if (textFlashcardFields.Length != 0)
                {
                    flashcard.Fields = new List<FlashcardFieldBase>(textFlashcardFields.Length);
                    flashcard.Fields.AddRange(textFlashcardFields);
                }

                var tags = (await _tagRelationRepository.GetFlashcardTags(flashcard.Id, _dbConnection, transaction)).ToList();
                if (tags.Count != 0)
                {
                    flashcard.Tags = new List<Tag>(tags.Count);
                    flashcard.Tags.AddRange(tags);
                }
            }

            transaction.Commit();
            return flashcardCores;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<Flashcard> UpdateFlashcard(int id, Flashcard newValue)
    {
        using var transaction = _dbConnection.BeginTransaction();

        try
        {
            var flashcard = await _flashcardCoreRepository.Update(id, newValue, _dbConnection, transaction);

            newValue.Fields ??= [];
            flashcard.Fields = new List<FlashcardFieldBase>();
            flashcard.Fields.AddRange(await UpdateTextFields(id, newValue.Fields.OfType<TextFlashcardField>().ToList(), _dbConnection, transaction));
            if (flashcard.Fields.Count == 0)
            {
                flashcard.Fields = null;
            }

            newValue.Tags ??= [];
            flashcard.Tags = new List<Tag>();
            flashcard.Tags.AddRange(await UpdateTags(flashcard.UserId, flashcard.Id, newValue.Tags, _dbConnection, transaction));
            if (flashcard.Tags.Count == 0)
            {
                flashcard.Tags = null;
            }

            transaction.Commit();
            return flashcard;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task DeleteFlashcard(int id)
    {
        using var transaction = _dbConnection.BeginTransaction();
        try
        {
            await _flashcardCoreRepository.Delete(id, _dbConnection, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task OnCorrectRevision(int id)
    {
        var statistics = await _statisticsRepository.GetByFlashcardId(id, _dbConnection);
        statistics.LastAnswerCorrect = true;
        statistics.LastTimeRevisedUtc = DateTime.UtcNow;
        statistics.FlashCardBox = Math.Min(statistics.FlashCardBox + 1, Domain.Flashcards.FlashcardStatistics.MaxFlashCardBox);

        await _statisticsRepository.Update(id, statistics, _dbConnection);
    }

    public async Task OnIncorrectRevision(int id)
    {
        var statistics = await _statisticsRepository.GetByFlashcardId(id, _dbConnection);
        statistics.LastAnswerCorrect = false;
        statistics.LastTimeRevisedUtc = DateTime.UtcNow;
        statistics.FlashCardBox = Math.Max(statistics.FlashCardBox - 1, Domain.Flashcards.FlashcardStatistics.MinFlashCardBox);

        await _statisticsRepository.Update(id, statistics, _dbConnection);
    }

    private async Task<IEnumerable<FlashcardFieldBase>> UpdateTextFields(int flashcardId,
        List<TextFlashcardField> fields, IDbConnection connection, IDbTransaction? transaction=null)
    {
        var fieldsInDataBase = (await _textFieldRepository.GetFlashcardTextFields(flashcardId, connection))
            .Select(x => x.Id!.Value).ToHashSet();
        
        var fieldsWithIdInNewState = fields
            .Where(x => x.Id is not null)
            .Select(x => x.Id!.Value)
            .ToHashSet();
        
        foreach (var id in fieldsInDataBase.Except(fieldsWithIdInNewState))
        {
            await _textFieldRepository.Delete(id, connection, transaction);
        }

        List<TextFlashcardField> updated = new(fields.Count);
        foreach (var field in fields)
        {
            updated.Add(
                field.Id is null 
                    ? await _textFieldRepository.Create(flashcardId, field.Name, field.CanBeShownInQuestion, field.Value, connection, transaction)
                    : await _textFieldRepository.Update(field.Id!.Value, field, connection, transaction)
            );
        }

        return updated;
    }

    private async Task<IEnumerable<Tag>> UpdateTags(int userId, int flashcardId, List<Tag> currentTags, IDbConnection connection,
        IDbTransaction? transaction = null)
    {
        var tagsInDb = (await _tagRelationRepository.GetFlashcardTags(flashcardId, connection, transaction))
            .Select(x => x.Id!.Value)
            .ToHashSet();

        var tagsCurrent = currentTags
            .Where(x => x.Id is not null)
            .Select(x => x.Id!.Value)
            .ToHashSet();

        var tagsToDelete = tagsInDb.Except(tagsCurrent);
        foreach (var id in tagsToDelete)
        {
            await _tagRelationRepository.DeleteRelation(flashcardId, id, connection, transaction);
        }

        List<Tag> result = new(currentTags.Count);
        foreach (var tag in currentTags)
        {
            if (tag.Id is null)
            {
                var newTag = await _tagRepository.Create(userId, tag.Name, connection, transaction);
                await _tagRelationRepository.CreateRelation(flashcardId, newTag.Id!.Value, connection, transaction);
                result.Add(newTag);
            }
            else 
            {
                if (tagsInDb.Contains(tag.Id.Value) is false)
                {
                    await _tagRelationRepository.CreateRelation(flashcardId, tag.Id.Value, connection, transaction);
                }
                
                result.Add(tag);
            }
        }

        return result;
    }
}