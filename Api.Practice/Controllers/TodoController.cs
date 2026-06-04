namespace Api.Practice.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Practice.Resources;
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
    [ProducesResponseType(typeof(IEnumerable<TodoItemResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTodos(CancellationToken cancellationToken)
    {
        var todos = await this.todoService.GetTodosAsync();

        var response = todos.Select(t => new TodoItemResponse
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            IsCompleted = t.IsCompleted
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTodoById(int id, CancellationToken cancellationToken)
    {
        var todo = await this.todoService.GetTodoByIdAsync(id);

        if (todo is null)
            return NotFound();

        var response = new TodoItemResponse
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted
        };

        return Ok(response);
    }
}
