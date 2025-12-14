namespace TodoFilter.Razor.Models;

/// <summary>
/// Represents a todo item displayed in the Blazor UI.
/// Mirrors the DTO used in the console/test demo so differences are easy to spot.
/// </summary>
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
