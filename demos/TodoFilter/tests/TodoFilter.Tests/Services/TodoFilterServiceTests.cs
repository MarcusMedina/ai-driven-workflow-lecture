using TodoFilter.Models;
using TodoFilter.Services;
using Xunit;

namespace TodoFilter.Tests.Services;

/// <summary>
/// Tests for TodoFilterService - demonstrating TDD approach
/// Written BEFORE implementation
/// </summary>
public class TodoFilterServiceTests
{
    private readonly TodoFilterService _service;

    public TodoFilterServiceTests()
    {
        _service = new TodoFilterService();
    }

    #region FilterByStatus Tests

    [Fact]
    public void FilterByStatus_WithMatchingStatus_ReturnsFilteredList()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Task 1", Status = "done" },
            new Todo { Id = 2, Title = "Task 2", Status = "pending" },
            new Todo { Id = 3, Title = "Task 3", Status = "done" }
        };

        // Act
        var result = _service.FilterByStatus(todos, "done");

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, t => Assert.Equal("done", t.Status));
    }

    [Fact]
    public void FilterByStatus_WithNoMatches_ReturnsEmptyList()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Task 1", Status = "done" },
            new Todo { Id = 2, Title = "Task 2", Status = "pending" }
        };

        // Act
        var result = _service.FilterByStatus(todos, "archived");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FilterByStatus_WithEmptyList_ReturnsEmptyList()
    {
        // Arrange
        var todos = new List<Todo>();

        // Act
        var result = _service.FilterByStatus(todos, "done");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FilterByStatus_WithNullList_ReturnsEmptyList()
    {
        // Act
        var result = _service.FilterByStatus(null, "done");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void FilterByStatus_WithNullStatus_ReturnsEmptyList()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Task 1", Status = "done" }
        };

        // Act
        var result = _service.FilterByStatus(todos, null);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FilterByStatus_IsCaseInsensitive()
    {
        // Arrange - users expect "Done" = "done"
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Task 1", Status = "Done" },
            new Todo { Id = 2, Title = "Task 2", Status = "DONE" },
            new Todo { Id = 3, Title = "Task 3", Status = "done" }
        };

        // Act
        var result = _service.FilterByStatus(todos, "done");

        // Assert
        Assert.Equal(3, result.Count);
    }

    #endregion

    #region FilterByDateAfter Tests

    [Fact]
    public void FilterByDateAfter_WithMatchingDates_ReturnsFilteredList()
    {
        // Arrange
        var cutoffDate = new DateTime(2024, 1, 1);
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Old", CreatedAt = new DateTime(2023, 12, 31) },
            new Todo { Id = 2, Title = "New", CreatedAt = new DateTime(2024, 1, 2) },
            new Todo { Id = 3, Title = "Newer", CreatedAt = new DateTime(2024, 1, 15) }
        };

        // Act
        var result = _service.FilterByDateAfter(todos, cutoffDate);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, t => Assert.True(t.CreatedAt > cutoffDate));
    }

    [Fact]
    public void FilterByDateAfter_WithNullList_ReturnsEmptyList()
    {
        // Act
        var result = _service.FilterByDateAfter(null, DateTime.UtcNow);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    #endregion

    #region FilterByTitleContains Tests

    [Fact]
    public void FilterByTitleContains_WithMatches_ReturnsFilteredList()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Buy milk" },
            new Todo { Id = 2, Title = "Buy bread" },
            new Todo { Id = 3, Title = "Clean house" }
        };

        // Act
        var result = _service.FilterByTitleContains(todos, "buy");

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, t => Assert.Contains("buy", t.Title, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void FilterByTitleContains_IsCaseInsensitive()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "BUY milk" },
            new Todo { Id = 2, Title = "buy bread" }
        };

        // Act
        var result = _service.FilterByTitleContains(todos, "BuY");

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void FilterByTitleContains_WithNullInputs_ReturnsEmptyList()
    {
        // Act & Assert
        Assert.Empty(_service.FilterByTitleContains(null, "test"));
        Assert.Empty(_service.FilterByTitleContains(new List<Todo>(), null));
    }

    #endregion
}
