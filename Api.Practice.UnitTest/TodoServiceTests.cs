namespace Api.Practice.UnitTest;

using Api.Practice.Dtos;
using Api.Practice.Entities;
using Api.Practice.Repositories;
using Api.Practice.Services;
using Api.Practice.Validations;
using AwesomeAssertions;
using NSubstitute;

public class TodoServiceTests
{
    private readonly ITodoRepository todoRepository;
    private readonly IValidation<TodoItemDto> validation;
    private readonly TodoService todoService;

    public TodoServiceTests()
    {
        this.todoRepository = Substitute.For<ITodoRepository>();
        this.validation = Substitute.For<IValidation<TodoItemDto>>();
        this.todoService = new TodoService(this.todoRepository, this.validation);
    }

    [Fact]
    public async Task CreateAsync_WithValidDto_ReturnsId()
    {
        // Arrange
        var dto = new TodoItemDto { Title = "Buy groceries", Description = "Milk and eggs" };
        var createdItem = new TodoItem { Id = 1, Title = dto.Title };

        this.todoRepository.CreateAsync(Arg.Any<TodoItem>()).Returns(Task.FromResult(createdItem));

        // Act
        var result = await this.todoService.CreateAsync(dto);

        // Assert
        result.Should().Be(1);
    }
}
