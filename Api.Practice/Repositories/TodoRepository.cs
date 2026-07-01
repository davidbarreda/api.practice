namespace Api.Practice.Repositories;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Practice.Data;
using Api.Practice.Entities;
using Microsoft.EntityFrameworkCore;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext context;

    public TodoRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<TodoItem>> GetAllAsync()
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

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        var todos = await this.context.TodoItems
            .Include(t => t.Tags)
            .ToListAsync();

        return todos.FirstOrDefault(t => t.Id == id);
    }

    public async Task<TodoItem> CreateAsync(TodoItem todoItem)
    {
        this.context.TodoItems.Add(todoItem);
        await this.context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<TodoItem?> UpdateAsync(int id, TodoItem todoItem)
    {
        var existing = await this.context.TodoItems.FindAsync(id);

        if (existing is null)
            return null;

        existing.Title = todoItem.Title;
        existing.Description = todoItem.Description;
        existing.IsCompleted = todoItem.IsCompleted;

        await this.context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await this.context.TodoItems.FindAsync(id);

        if (existing is null)
            return false;

        this.context.TodoItems.Remove(existing);
        await this.context.SaveChangesAsync();
        return true;
    }
}
