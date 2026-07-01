namespace Api.Practice.Controllers;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Practice.Dtos;
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
        return Ok(todos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var todo = await this.todoService.GetByIdAsync(id);

        if (todo is null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] TodoItemDto request, CancellationToken cancellationToken)
    {
        var id = await this.todoService.CreateAsync(request);
        return Ok(id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] TodoItemDto request, CancellationToken cancellationToken)
    {
        var todo = await this.todoService.UpdateAsync(id, request);

        if (todo is null)
            return NotFound();

        return Ok(todo);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await this.todoService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
