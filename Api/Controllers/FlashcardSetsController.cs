// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using ApiRequestModels;
using Application.Flashcards.FlashcardSetCollectors.Learning;
using Application.Flashcards.FlashcardSetCollectors.Revising;
using Domain.Flashcards;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("flashcards/sets")]
public class FlashcardSetsController : ControllerBase
{
    private readonly ILearningFlashcardSetCollector _learningFlashcardSetCollector;
    private readonly IRevisionFlashcardSetCollector _revisionFlashcardSetCollector;

    public FlashcardSetsController(ILearningFlashcardSetCollector learningFlashcardSetCollector, IRevisionFlashcardSetCollector revisionFlashcardSetCollector)
    {
        _learningFlashcardSetCollector = learningFlashcardSetCollector;
        _revisionFlashcardSetCollector = revisionFlashcardSetCollector;
    }

    [HttpGet("revision")]
    public async Task<IEnumerable<Flashcard>> GetRevisionSet([FromQuery] FlashcardSetRequest request)
    {
        return await _revisionFlashcardSetCollector.GetFlashcardsForRevision(request.UserId, request.FlashcardsAmount);
    }
    
    [HttpGet("learning")]
    public async Task<IEnumerable<Flashcard>> GetLearningSet([FromQuery] FlashcardSetRequest request)
    {
        return await _learningFlashcardSetCollector.GetFlashcardsForLearning(request.UserId, request.FlashcardsAmount);
    }
}