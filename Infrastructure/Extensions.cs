// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 26.3.2024

using Application.Auth;
using Application.Flashcards.FlashcardFields.Repositories;
using Application.Flashcards.FlashcardFields.Services;
using Application.Flashcards.FlashcardSetCollectors.Learning;
using Application.Flashcards.FlashcardSetCollectors.Revising;
using Application.Flashcards.Repositories;
using Application.Flashcards.Services;
using Application.FlashcardStatistics.Services;
using Application.FlashcardStatistics.Services.ForgetfulnessProbability;
using Application.Repositories;
using Application.Services;
using Application.Users.Repositories;
using Application.Users.Services;
using Dapper;
using Infrastructure.Auth;
using Infrastructure.Auth.Services;
using Infrastructure.Flashcards.FlashcardFields.TextFields.Repositories;
using Infrastructure.Flashcards.FlashcardFields.TextFields.Services;
using Infrastructure.Flashcards.FlashcardSetCollectors;
using Infrastructure.Flashcards.FlashcardSetCollectors.Learning;
using Infrastructure.Flashcards.FlashcardStatistics.Repositories;
using Infrastructure.Flashcards.FlashcardStatistics.Services;
using Infrastructure.Flashcards.FlashcardStatistics.Services.ForgetfulnessProbability;
using Infrastructure.Flashcards.Repositories;
using Infrastructure.Flashcards.Services;
using Infrastructure.Flashcards.Tags.Repositories;
using Infrastructure.Flashcards.Tags.Services;
using Infrastructure.Users.Repositories;
using Infrastructure.Users.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection collection)
    {
        return collection
            .AddScoped<IUserRepository, UserRepositoryPostgreSql>()
            .AddScoped<IFlashcardCoreRepository, FlashcardCoreRepositoryPostgreSql>()
            .AddScoped<ITextFlashcardFieldRepository, TextFlashcardFieldRepositoryPosgreSql>()
            .AddScoped<IFlashcardStatisticsRepository, FlashcardStatisticsRepositoryPostgreSql>()
            .AddScoped<ITagRepository, TagRepositoryPostgreSql>()
            .AddScoped<ITokenRepository, TokenRepositoryRedis>();
    }

    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        return collection
            .AddScoped<IUserService, UserService>()
            .AddScoped<IFlashcardService, FlashcardService>()
            .AddScoped<ITextFlashcardFieldService, TextFlashcardFieldService>()
            .AddScoped<IFlashcardStatisticsService, FlashcardStatisticsService>()
            .AddScoped<IForgetfulnessProbabilityService, EbbinghausForgetfulnessProbabilityService>()
            .AddScoped<IRevisionFlashcardSetCollector, RevisionFlashcardSetCollector>()
            .AddScoped<ILearningFlashcardSetCollector, LearningFlashcardSetCollector>()
            .AddScoped<ITagService, TagService>()
            .AddScoped<IFlashcardTagRelationRepository, FlashcardTagRelationRepositoryPostgreSql>()
            .AddScoped<IAuthService, AuthService>();
    }
}