namespace TodoApp.Models;

public record TodoItem(Guid Id, string Text, bool Completed, DateTimeOffset CreatedAt, string Category);
