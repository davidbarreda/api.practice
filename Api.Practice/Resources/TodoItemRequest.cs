namespace Api.Practice.Resources;

public class TodoItemRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}
