namespace Api.Practice.UnitTest;

using Api.Practice.Dtos;
using Api.Practice.Validations;
using AwesomeAssertions;

public class TodoItemValidationTests
{
    private readonly TodoItemValidation todoItemValidation;

    public TodoItemValidationTests()
    {
        this.todoItemValidation = new TodoItemValidation();
    }

    [Theory]
    [InlineData("Buy groceries", true)]
    [InlineData("", false)]
    public void Validate_todo_items(string title, bool expected)
    {
        // Arrange
        var request = new TodoItemDto
        {
            Title = title
        };

        // Act
        var result = this.todoItemValidation.IsValid(request);

        // Assert
        result.Should().Be(expected);
    }
}
