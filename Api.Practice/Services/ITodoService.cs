namespace Api.Practice.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Practice.Entities;

public interface ITodoService
{
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<TodoItem> CreateAsync(TodoItem todoItem);
    Task<TodoItem?> UpdateAsync(int id, TodoItem todoItem);
    Task<bool> DeleteAsync(int id);
}
