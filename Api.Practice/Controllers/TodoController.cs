namespace Api.Practice.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Practice.Dtos;
using Api.Practice.Entities;
using Api.Practice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoService todoService;

    public TodoController(ITodoService todoService)
    {
        this.todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var todos = await this.todoService.GetAllAsync();

        var response = todos.Select(t => new TodoItemDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            IsCompleted = t.IsCompleted
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var todo = await this.todoService.GetByIdAsync(id);

        if (todo is null)
            return NotFound();

        var response = new TodoItemDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted
        };

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] TodoItemDto request, CancellationToken cancellationToken)
    {
        var todo = await this.todoService.CreateAsync(new TodoItem
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = request.IsCompleted
        });

        return Ok(todo.Id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] TodoItemDto request, CancellationToken cancellationToken)
    {
        var todo = await this.todoService.UpdateAsync(id, new TodoItem
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = request.IsCompleted
        });

        if (todo is null)
            return NotFound();

        var response = new TodoItemDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await this.todoService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return Ok(deleted);
    }
}
