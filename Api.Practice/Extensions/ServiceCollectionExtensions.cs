namespace Api.Practice.Extensions;

using Api.Practice.Data;
using Api.Practice.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("TodoDb"));

        services.AddSingleton<IForecastService, ForecastService>();
        services.AddScoped<ITodoService, TodoService>();

        return services;
    }
}
