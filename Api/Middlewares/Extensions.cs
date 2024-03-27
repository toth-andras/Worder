// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

namespace Api.Middlewares;

public static class Extensions
{
    public static IServiceCollection AddMiddlewares(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseMiddlewares(this WebApplication app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}