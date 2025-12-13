namespace TodoFilter.Models;

/// <summary>
/// Represents a todo item
/// </summary>
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
