// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 26.3.2024

using System.Data;
using Application.Flashcards.Repositories;
using Application.Flashcards.Services;
using Domain.Flashcards;

namespace Infrastructure.Flashcards.Services;

public class FlashcardService : IFlashcardService
{
    private IDbConnection _dbConnection;
    private IFlashcardCoreRepository _flashcardCoreRepository;

    public FlashcardService(IDbConnection connection, IFlashcardCoreRepository flashcardCoreRepository)
    {
        _dbConnection = connection;
        _flashcardCoreRepository = flashcardCoreRepository;
    }

    public async Task<Flashcard> CreateFlashcard(int userId, string term, string definition)
    {
        using var transaction = _dbConnection.BeginTransaction();
        Flashcard flashcard;

        try
        {
            flashcard = await _flashcardCoreRepository.Create(userId, term, definition, _dbConnection, transaction);

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
            var flashcard = await _flashcardCoreRepository.GetById(id, _dbConnection);
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
            var flashcardCores = await _flashcardCoreRepository.GetByUserId(userId, _dbConnection, transaction);
            
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
}