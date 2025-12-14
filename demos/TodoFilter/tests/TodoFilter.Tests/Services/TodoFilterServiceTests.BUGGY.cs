using TodoFilter.Models;
using TodoFilter.Services;
using Xunit;

namespace TodoFilter.Tests.Services;

/// <summary>
/// ⚠️ BUGGY VERSION - FÖR DEMO-SYFTE
/// Dessa tester KÖR vi mot den buggy implementationen för att visa hur TDD hittar buggar!
///
/// FÖRVÄNTAT RESULTAT NÄR VI KÖR DESSA TESTER MOT BUGGY KOD:
/// ❌ FilterByStatus_WithNullStatus_ReturnsEmptyList → KRASCH (NullReferenceException)
/// ❌ FilterByStatus_IsCaseInsensitive → FAILAR (förväntar 3, får 0)
/// </summary>
public class TodoFilterServiceTests_BUGGY
{
    private readonly TodoFilterService_BUGGY _service;

    public TodoFilterServiceTests_BUGGY()
    {
        _service = new TodoFilterService_BUGGY();
    }

    #region FilterByStatus Tests - DESSA KOMMER FAILA!

    /// <summary>
    /// ❌ Detta test kommer KRASCHA med NullReferenceException!
    /// Buggy koden gör ingen null-check på status
    /// </summary>
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
        Assert.Empty(result);  // ❌ KRASCHAR INNAN DEN KOMMER HIT!
    }

    /// <summary>
    /// ❌ Detta test kommer FAILA!
    /// Buggy koden använder case-sensitive jämförelse
    /// Förväntar: 3 todos
    /// Får: 0 todos (eftersom "Done", "DONE" != "done")
    /// </summary>
    [Fact]
    public void FilterByStatus_IsCaseInsensitive()
    {
        // Arrange - realistiskt scenario: användare skriver olika varianter
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Task 1", Status = "Done" },
            new Todo { Id = 2, Title = "Task 2", Status = "DONE" },
            new Todo { Id = 3, Title = "Task 3", Status = "done" }
        };

        // Act - användaren söker med "done" (lowercase)
        var result = _service.FilterByStatus(todos, "done");

        // Assert
        Assert.Equal(3, result.Count);  // ❌ FAILAR! Faktisk count = 1
        // Endast den med exakt "done" hittas → vi tappar 2 av 3 todos!
    }

    /// <summary>
    /// ✅ Detta test FUNKAR även med buggy kod
    /// (eftersom vi använder samma case)
    /// </summary>
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
        Assert.Equal(2, result.Count);  // ✅ PASSAR - samma case!
        Assert.All(result, t => Assert.Equal("done", t.Status));
    }

    /// <summary>
    /// ✅ Detta test FUNKAR även med buggy kod
    /// </summary>
    [Fact]
    public void FilterByStatus_WithNullList_ReturnsEmptyList()
    {
        // Act
        var result = _service.FilterByStatus(null, "done");

        // Assert
        Assert.NotNull(result);  // ✅ PASSAR
        Assert.Empty(result);
    }

    #endregion
}
