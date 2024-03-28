// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.3.2024

using ApiRequestModels;
using Application.Services;
using Domain.Flashcards;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("flashcards/tags")]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }
    
    [HttpPost("create")]
    public async Task<Tag> CreateTag([FromBody] CreateTagRequest request)
    {
        return await _tagService.Create(request.UserId, request.TagName);
    }

    [HttpGet]
    public async Task<IEnumerable<Tag>> GetUserTags([FromQuery] GetUserTagsRequest request)
    {
        return await _tagService.GetUserTags(request.UserId);
    }

    [HttpPut("update")]
    public async Task<Tag> UpdateTag([FromBody] UpdateTagRequest request)
    {
        return await _tagService.Update(request.TagId, request.UserId, request.NewValue);
    }

    [HttpDelete("delete")]
    public async Task Delete([FromBody] DeleteTagRequest request)
    {
        await _tagService.Delete(request.TagId);
    }
}