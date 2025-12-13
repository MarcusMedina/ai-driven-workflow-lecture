using TodoFilter.Models;

namespace TodoFilter.Services;

/// <summary>
/// Service for filtering todos by various criteria
/// Demonstrates: SRP, DRY, KISS principles
/// </summary>
public class TodoFilterService
{
    /// <summary>
    /// Filters todos by status (case-insensitive)
    /// </summary>
    /// <param name="todos">List of todos to filter</param>
    /// <param name="status">Status to filter by (e.g., "done", "pending")</param>
    /// <returns>Filtered list of todos</returns>
    public List<Todo> FilterByStatus(List<Todo> todos, string status)
    {
        // Edge case: null or empty inputs
        if (todos == null || string.IsNullOrEmpty(status))
            return new List<Todo>();

        // Case-insensitive matching - users expect "Done" = "done"
        return todos
            .Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Filters todos created after a specific date
    /// </summary>
    public List<Todo> FilterByDateAfter(List<Todo> todos, DateTime date)
    {
        if (todos == null)
            return new List<Todo>();

        return todos
            .Where(t => t.CreatedAt > date)
            .ToList();
    }

    /// <summary>
    /// Filters todos by title containing search term (case-insensitive)
    /// </summary>
    public List<Todo> FilterByTitleContains(List<Todo> todos, string searchTerm)
    {
        if (todos == null || string.IsNullOrEmpty(searchTerm))
            return new List<Todo>();

        return todos
            .Where(t => t.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
