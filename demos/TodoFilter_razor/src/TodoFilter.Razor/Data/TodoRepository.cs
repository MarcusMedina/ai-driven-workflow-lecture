using TodoFilter.Razor.Models;

namespace TodoFilter.Razor.Data;

/// <summary>
/// Static seed data so the demo can run without a backend or database.
/// </summary>
public class TodoRepository
{
    private readonly List<Todo> _seed = new()
    {
        new Todo { Id = 1, Title = "Write TDD tests", Status = "Pending", CreatedAt = DateTime.UtcNow.AddDays(-3) },
        new Todo { Id = 2, Title = "Implement FilterByStatus", Status = "In Progress", CreatedAt = DateTime.UtcNow.AddDays(-2) },
        new Todo { Id = 3, Title = "Demo in Blazor", Status = "Done", CreatedAt = DateTime.UtcNow.AddDays(-1) },
        new Todo { Id = 4, Title = "Document workflow", Status = "Done", CreatedAt = DateTime.UtcNow.AddDays(-4) },
        new Todo { Id = 5, Title = "Refactor service", Status = "Pending", CreatedAt = DateTime.UtcNow.AddDays(-6) },
        new Todo { Id = 6, Title = "Publish slides", Status = "Blocked", CreatedAt = DateTime.UtcNow.AddDays(-7) },
        new Todo { Id = 7, Title = "Prep Q&A", Status = "In Progress", CreatedAt = DateTime.UtcNow.AddDays(-5) },
    };

    public List<Todo> GetAll() =>
        _seed.Select(t => new Todo
        {
            Id = t.Id,
            Title = t.Title,
            Status = t.Status,
            CreatedAt = t.CreatedAt
        }).ToList();

    public IReadOnlyList<string> Statuses =>
        _seed.Select(t => t.Status).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(s => s).ToList();
}
