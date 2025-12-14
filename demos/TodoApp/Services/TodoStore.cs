using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using TodoApp.Models;

namespace TodoApp.Services;

public class TodoStore
{
    private readonly string _storagePath;
    private readonly SemaphoreSlim _mutex = new(1, 1);
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };
    private List<TodoItem> _items = new();
    private bool _loaded;

    public TodoStore(IWebHostEnvironment environment)
    {
        var dataDirectory = Path.Combine(environment.ContentRootPath, "Data");
        Directory.CreateDirectory(dataDirectory);
        _storagePath = Path.Combine(dataDirectory, "todos.json");
    }

    public async Task<IReadOnlyList<TodoItem>> GetAllAsync()
    {
        await EnsureLoadedAsync();
        await _mutex.WaitAsync();
        try
        {
            return _items
                .OrderByDescending(todo => todo.CreatedAt)
                .ToList();
        }
        finally
        {
            _mutex.Release();
        }
    }

    public async Task<TodoItem> AddAsync(string text, string category)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Todo text is required.", nameof(text));
        }
        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException("Category is required.", nameof(category));
        }

        await EnsureLoadedAsync();
        var trimmed = text.Trim();

        await _mutex.WaitAsync();
        try
        {
            var item = new TodoItem(Guid.NewGuid(), trimmed, false, DateTimeOffset.UtcNow, category);
            _items.Add(item);
            await PersistLockedAsync();
            return item;
        }
        finally
        {
            _mutex.Release();
        }
    }

    public async Task<TodoItem?> ToggleAsync(Guid id)
    {
        await EnsureLoadedAsync();
        await _mutex.WaitAsync();
        try
        {
            var index = _items.FindIndex(todo => todo.Id == id);
            if (index == -1)
            {
                return null;
            }

            var updated = _items[index] with { Completed = !_items[index].Completed };
            _items[index] = updated;
            await PersistLockedAsync();
            return updated;
        }
        finally
        {
            _mutex.Release();
        }
    }

    private async Task EnsureLoadedAsync()
    {
        if (_loaded)
        {
            return;
        }

        await _mutex.WaitAsync();
        try
        {
            if (_loaded)
            {
                return;
            }

            if (File.Exists(_storagePath))
            {
                var json = await File.ReadAllTextAsync(_storagePath);
                var deserialized = string.IsNullOrWhiteSpace(json)
                    ? new List<TodoItem>()
                    : JsonSerializer.Deserialize<List<TodoItem>>(json, _jsonOptions) ?? new List<TodoItem>();

                _items = deserialized
                    .Select(item => item with { Category = string.IsNullOrWhiteSpace(item.Category) ? TodoService.DefaultCategory : item.Category })
                    .ToList();
            }

            _loaded = true;
        }
        finally
        {
            _mutex.Release();
        }
    }

    private async Task PersistLockedAsync()
    {
        var json = JsonSerializer.Serialize(_items, _jsonOptions);
        await File.WriteAllTextAsync(_storagePath, json);
    }
}
