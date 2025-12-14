using TodoFilter.Razor.Models;

namespace TodoFilter.Razor.Services;

/// <summary>
/// Pure filtering logic reused from the console/test demo.
/// Keeping this headless makes it trivial to unit test or swap UI later.
/// </summary>
public class TodoFilterService
{
    public List<Todo> FilterByStatus(List<Todo> todos, string status)
    {
        if (todos == null || string.IsNullOrEmpty(status))
            return new List<Todo>();

        return todos
            .Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Todo> FilterByDateAfter(List<Todo> todos, DateTime date)
    {
        if (todos == null)
            return new List<Todo>();

        return todos
            .Where(t => t.CreatedAt > date)
            .ToList();
    }

    public List<Todo> FilterByTitleContains(List<Todo> todos, string searchTerm)
    {
        if (todos == null || string.IsNullOrEmpty(searchTerm))
            return new List<Todo>();

        return todos
            .Where(t => t.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
