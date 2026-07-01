namespace Api.Practice.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Practice.Dtos;
using Api.Practice.Entities;
using Api.Practice.Repositories;

public class TodoService : ITodoService
{
    private readonly ITodoRepository todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        this.todoRepository = todoRepository;
    }

    public async Task<List<TodoItemDto>> GetAllAsync()
    {
        var items = await this.todoRepository.GetAllAsync();
        return items.Select(MapToDto).ToList();
    }

    public async Task<TodoItemDto?> GetByIdAsync(int id)
    {
        var item = await this.todoRepository.GetByIdAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<int> CreateAsync(TodoItemDto dto)
    {
        var item = await this.todoRepository.CreateAsync(MapToEntity(dto));
        return item.Id;
    }

    public async Task<TodoItemDto?> UpdateAsync(int id, TodoItemDto dto)
    {
        var item = await this.todoRepository.UpdateAsync(id, MapToEntity(dto));
        return item is null ? null : MapToDto(item);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return this.todoRepository.DeleteAsync(id);
    }

    private static TodoItemDto MapToDto(TodoItem item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        Description = item.Description,
        IsCompleted = item.IsCompleted
    };

    private static TodoItem MapToEntity(TodoItemDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        IsCompleted = dto.IsCompleted
    };
}
