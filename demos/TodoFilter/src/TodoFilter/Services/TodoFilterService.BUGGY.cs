using TodoFilter.Models;

namespace TodoFilter.Services;

/// <summary>
/// ⚠️ BUGGY VERSION - FÖR DEMO-SYFTE
/// Denna version innehåller avsiktliga buggar som testerna ska hitta!
///
/// BUGGAR:
/// 1. Ingen null-check på status parameter → NullReferenceException
/// 2. Case-sensitive jämförelse → "Done" hittas inte när vi söker "done"
/// </summary>
public class TodoFilterService_BUGGY
{
    /// <summary>
    /// ⚠️ BUGGY: Filters todos by status (CASE-SENSITIVE och ingen null-check!)
    /// </summary>
    public List<Todo> FilterByStatus(List<Todo> todos, string status)
    {
        // BUG #1: Ingen null-check på status!
        // Om någon anropar FilterByStatus(todos, null) → KRASCH!

        if (todos == null)
            return new List<Todo>();

        // BUG #2: Använder == istället för case-insensitive jämförelse
        // "Done" != "done" → tappar data!
        return todos
            .Where(t => t.Status == status)  // ❌ DÅLIGT: Case-sensitive!
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
