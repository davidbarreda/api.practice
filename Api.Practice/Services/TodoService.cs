namespace Api.Practice.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Practice.Entities;

public class TodoService : ITodoService
{
    private static readonly List<TodoItem> _todos =
    [
        new TodoItem { Id = 1, Title = "Buy groceries", Description = "Milk, bread and eggs", IsCompleted = false },
        new TodoItem { Id = 2, Title = "Read a book", Description = "Finish the current chapter", IsCompleted = false },
        new TodoItem { Id = 3, Title = "Go for a run", Description = "30 minutes in the park", IsCompleted = true },
    ];

    public Task<List<TodoItem>> GetTodosAsync()
    {
        return Task.FromResult(_todos);
    }

    public Task<TodoItem?> GetTodoByIdAsync(int id)
    {
        return Task.FromResult(_todos.FirstOrDefault(t => t.Id == id));
    }
}
