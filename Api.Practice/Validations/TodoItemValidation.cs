namespace Api.Practice.Validations;

using System.Threading.Tasks;
using Api.Practice.Dtos;

public class TodoItemValidation : IValidation<TodoItemDto>
{
    public Task<bool> IsValid(TodoItemDto request)
    {
        return Task.FromResult(
            request.Title.Length > 0 &&
            request.Description.Length > 0);
    }
}
