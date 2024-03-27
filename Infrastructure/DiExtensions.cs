// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 26.3.2024

using Application.Flashcards.Repositories;
using Application.Flashcards.Services;
using Application.Users.Repositories;
using Application.Users.Services;
using Infrastructure.Flashcards.Repositories;
using Infrastructure.Flashcards.Services;
using Infrastructure.Users.Repositories;
using Infrastructure.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DiExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection collection)
    {
        return collection
            .AddScoped<IUserRepository, UserRepositoryPostgreSql>()
            .AddScoped<IFlashcardCoreRepository, FlashcardCoreRepositoryPostgreSql>();
    }

    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        return collection
            .AddScoped<IUserService, UserService>()
            .AddScoped<IFlashcardService, FlashcardService>();
    }
}