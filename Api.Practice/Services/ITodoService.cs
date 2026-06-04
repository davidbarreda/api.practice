namespace Api.Practice.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Practice.Entities;

public interface ITodoService
{
    Task<List<TodoItem>> GetTodosAsync();
    Task<TodoItem?> GetTodoByIdAsync(int id);
}
