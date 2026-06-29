namespace Api.Practice.Extensions;

using Api.Practice.Data;
using Api.Practice.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class WebApplicationExtensions
{
    public static WebApplication SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        db.TodoItems.AddRange(
            new TodoItem
            {
                Id = 1, Title = "Buy groceries", Description = "Milk, bread and eggs", IsCompleted = false,
                Tags = [ new Tag { Name = "shopping" }, new Tag { Name = "home" } ]
            },
            new TodoItem
            {
                Id = 2, Title = "Read a book", Description = "Finish the current chapter", IsCompleted = false,
                Tags = [ new Tag { Name = "personal" } ]
            },
            new TodoItem
            {
                Id = 3, Title = "Go for a run", Description = "30 minutes in the park", IsCompleted = true,
                Tags = [ new Tag { Name = "health" }, new Tag { Name = "outdoor" } ]
            }
        );

        db.SaveChanges();
        return app;
    }
}
