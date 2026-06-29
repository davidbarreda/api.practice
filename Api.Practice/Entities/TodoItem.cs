namespace Api.Practice.Entities;

using System.Collections.Generic;

public class TodoItem
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public ICollection<Tag> Tags { get; set; } = [];
}
