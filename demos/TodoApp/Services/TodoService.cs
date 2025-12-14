using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.RegularExpressions;
using TodoApp.Models;

namespace TodoApp.Services;

public class TodoService
{
    public const string DefaultCategory = "Allmänt";
    internal const int MaxTitleLength = 200;
    internal const int MaxCategoryLength = 50;
    private readonly TodoStore _store;
    private readonly HtmlEncoder _encoder;

    public TodoService(TodoStore store, HtmlEncoder? encoder = null)
    {
        _store = store;
        _encoder = encoder ?? EncodingFactory.CreateHtmlEncoder();
    }

    public async Task<TodoItem> CreateTodoAsync(string? title, string? category = null)
    {
        var sanitized = ValidateAndSanitizeTitle(title);
        var normalizedCategory = ValidateAndSanitizeCategory(category);
        return await _store.AddAsync(sanitized, normalizedCategory);
    }

    public async Task<TodoItem?> ToggleTodoAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        return await _store.ToggleAsync(id);
    }

    public async Task<IReadOnlyList<TodoItem>> QueryTodosAsync(string? search, TodoStatusFilter filter, string? category = null)
    {
        var normalizedSearch = NormalizeSearch(search);
        var normalizedCategory = NormalizeCategoryFilter(category);
        var items = await _store.GetAllAsync();

        IEnumerable<TodoItem> query = items;

        query = filter switch
        {
            TodoStatusFilter.Completed => query.Where(todo => todo.Completed),
            TodoStatusFilter.Open => query.Where(todo => !todo.Completed),
            _ => query
        };

        if (!string.IsNullOrEmpty(normalizedSearch))
        {
            query = query.Where(todo => todo.Text.Contains(normalizedSearch, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(normalizedCategory))
        {
            query = query.Where(todo => string.Equals(todo.Category, normalizedCategory, StringComparison.OrdinalIgnoreCase));
        }

        return query
            .OrderByDescending(todo => todo.CreatedAt)
            .ToList();
    }

    internal string ValidateAndSanitizeTitle(string? title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        var trimmed = title.Trim();
        if (trimmed.Length > MaxTitleLength)
        {
            throw new ArgumentException($"Title must be {MaxTitleLength} characters or fewer.", nameof(title));
        }

        var normalized = CollapseWhitespace(trimmed);
        return _encoder.Encode(normalized);
    }

    internal string ValidateAndSanitizeCategory(string? category)
    {
        var value = string.IsNullOrWhiteSpace(category) ? DefaultCategory : CollapseWhitespace(category.Trim());
        if (value.Length > MaxCategoryLength)
        {
            throw new ArgumentException($"Category must be {MaxCategoryLength} characters or fewer.", nameof(category));
        }

        return _encoder.Encode(value);
    }

    internal string? NormalizeCategoryFilter(string? category)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            return null;
        }

        var trimmed = category.Trim();
        if (trimmed.Equals("Alla", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (trimmed.Length > MaxCategoryLength)
        {
            throw new ArgumentException($"Category must be {MaxCategoryLength} characters or fewer.", nameof(category));
        }

        var normalized = CollapseWhitespace(trimmed);
        return _encoder.Encode(normalized);
    }

    internal string? NormalizeSearch(string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return null;
        }

        var trimmed = search.Trim();
        if (trimmed.Length > 100)
        {
            throw new ArgumentException("Search term is too long.", nameof(search));
        }

        return CollapseWhitespace(trimmed);
    }

    internal static string CollapseWhitespace(string value) =>
        Regex.Replace(value, "\\s+", " ").Trim();
}
