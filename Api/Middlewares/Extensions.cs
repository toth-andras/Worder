// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 27.3.2024

using System.Text;

namespace Api.Middlewares;

public static class Extensions
{
    public static IServiceCollection AddMiddlewares(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<ExceptionHandlingMiddleware>()
            .AddSingleton<RequestResponseLogMiddleware>();
    }

    public static IApplicationBuilder UseMiddlewares(this WebApplication app)
    {
        return app
            .UseMiddleware<ExceptionHandlingMiddleware>()
            .UseMiddleware<RequestResponseLogMiddleware>();
    }
    
    public static async Task<string> GetBodyAsStringAsync(this HttpRequest request)
    {
        request.EnableBuffering();
        
        using StreamReader reader = new(request.Body, leaveOpen: true);
        var bodyAsString = await reader.ReadToEndAsync();

        request.Body.Position = 0;
        return "Request body: " + bodyAsString;
    }
}