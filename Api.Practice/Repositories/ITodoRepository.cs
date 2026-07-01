namespace Api.Practice.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Practice.Entities;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<TodoItem> CreateAsync(TodoItem todoItem);
    Task<TodoItem?> UpdateAsync(int id, TodoItem todoItem);
    Task<bool> DeleteAsync(int id);
}
