namespace Api.Practice.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TodoItemId { get; set; }
    public TodoItem TodoItem { get; set; }
}
