namespace Api.Practice.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Practice.Dtos;
using Api.Practice.Entities;
using Api.Practice.Repositories;
using Api.Practice.Validations;

public class TodoService : ITodoService
{
    private readonly ITodoRepository todoRepository;
    private readonly IValidation<TodoItemDto> validation;

    public TodoService(ITodoRepository todoRepository, IValidation<TodoItemDto> validation)
    {
        this.todoRepository = todoRepository;
        this.validation = validation;
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
        var re = await validation.IsValid(dto);
        if (!await validation.IsValid(dto))
        {
            throw new ArgumentException("Invalid TodoItemDto2");
        }

        var item = await this.todoRepository.CreateAsync(MapToEntity(dto));
        return item.Id;
    }

    public async Task<TodoItemDto?> UpdateAsync(int id, TodoItemDto dto)
    {
        if (!await validation.IsValid(dto))
        {
            throw new ArgumentException("Invalid TodoItemDto");
        }

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
