// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 28.3.2024

using ApiRequestModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Validation.ApiRequestModels.FlashcardsController;
using Validation.ApiRequestModels.FlashcardSetCollectors;
using Validation.ApiRequestModels.TagsController;
using Validation.ApiRequestModels.UsersController;

namespace Validation;

public static class ServiceCollectionValidationExtensions
{
    public static IServiceCollection AddApiRequestModelValidators(this IServiceCollection collection)
    {
        return collection
            .AddScoped<IValidator<EmailPasswordRequest>, EmailPasswordRequestValidator>()
            .AddScoped<IValidator<EmailRequest>, EmailRequestValidator>()
            .AddScoped<IValidator<ChangePasswordRequest>, ChangePasswordRequestValidator>()
            .AddScoped<IValidator<ChangeEmailRequest>, ChangeEmailRequestValidator>()
            
            .AddScoped<IValidator<CreateFlaschardRequest>, CreateFlashcardRequestValidator>()
            .AddScoped<IValidator<UpdateFlashcardRequest>, UpdateFlashcardRequestValidator>()
            
            .AddScoped<IValidator<FlashcardSetRequest>, FlashcardSetRequestValidator>()
            
            .AddScoped<IValidator<CreateTagRequest>, CreateTagRequestValidator>()
            .AddScoped<IValidator<UpdateTagRequest>, UpdateTagRequestValidator>()
            
            .AddFluentValidationAutoValidation();
    }
}