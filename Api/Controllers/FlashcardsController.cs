// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 26.3.2024

using ApiRequestModels;
using Application.Flashcards.Repositories;
using Application.Flashcards.Services;
using Domain.Flashcards;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("flashcards")]
public class FlashcardsController : ControllerBase
{
    private readonly IFlashcardService _flashcardService;

    public FlashcardsController(IFlashcardService flashcardService)
    {
        _flashcardService = flashcardService;
    }
    
    [HttpPost("create")]
    public async Task<Flashcard> CreateFlashcard([FromBody] CreateFlaschardRequest request)
    {
        return await _flashcardService.CreateFlashcard(request.UserId, request.Term, request.Definition, request.Fields);
    }

    [HttpGet("{id:int}")]
    public async Task<Flashcard> GetFlashcard([FromRoute] int id)
    {
        return await _flashcardService.GetFlashcard(id);
    }

    [HttpGet("user/{id:int}")]
    public async Task<IEnumerable<Flashcard>> GetUserFlashcards([FromRoute(Name = "id")] int userId)
    {
        return await _flashcardService.GetUserFlashcards(userId);
    }

    [HttpPut("update")]
    public async Task<Flashcard> UpdateFlashcard([FromBody] UpdateFlashcardRequest request)
    {
        return await _flashcardService.UpdateFlashcard(request.FlashcardId, request.NewValue);
    }
    
    [HttpDelete("delete")]
    public async Task DeleteFlashcard(int id)
    {
        await _flashcardService.DeleteFlashcard(id);
    }
}