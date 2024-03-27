// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

using Application.Flashcards.FlashcardFields.Services;
using Domain.Flashcards;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("flashcards/fields/text")]
public class TextFlashcardFieldsController : ControllerBase
{
    private readonly ITextFlashcardFieldService _service;

    public TextFlashcardFieldsController(ITextFlashcardFieldService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<TextFlashcardField> CreateField(int flashcardId, string fieldName, bool canBeShownInQuestion, string fieldValue)
    {
        return await _service.CreateTextField(flashcardId, fieldName, canBeShownInQuestion, fieldValue);
    }

    [HttpDelete("delete")]
    public async Task DeleteTextField(int id)
    {
        await _service.DeleteTextField(id);
    }
}