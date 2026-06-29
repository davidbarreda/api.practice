namespace Api.Practice.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Practice.Data;
using Api.Practice.Entities;
using Microsoft.EntityFrameworkCore;

public class TodoService : ITodoService
{
    private readonly AppDbContext context;

    public TodoService(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<TodoItem>> GetTodosAsync()
    {
        return await this.context.TodoItems
            .Include(t => t.Tags)
            .Select(t => new TodoItem
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted
            })
            .ToListAsync();
    }

    public async Task<TodoItem?> GetTodoByIdAsync(int id)
    {
        var todos = await this.context.TodoItems
            .Include(t => t.Tags)
            .ToListAsync();

        return todos.FirstOrDefault(t => t.Id == id);
    }
}
