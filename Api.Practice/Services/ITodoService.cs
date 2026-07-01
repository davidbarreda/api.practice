namespace Api.Practice.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Practice.Dtos;

public interface ITodoService
{
    Task<List<TodoItemDto>> GetAllAsync();
    Task<TodoItemDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(TodoItemDto dto);
    Task<TodoItemDto?> UpdateAsync(int id, TodoItemDto dto);
    Task<bool> DeleteAsync(int id);
}
