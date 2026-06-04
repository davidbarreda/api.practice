namespace Api.Practice.Extensions;

using Api.Practice.Services;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {

        services.AddSingleton<IForecastService, ForecastService>();
        services.AddSingleton<ITodoService, TodoService>();
        return services;
    }
}
