using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Tests;

public class TodoServiceTests
{
    [Fact]
    public async Task CreateTodo_WithValidTitle_CreatesTodo()
    {
        var service = CreateService();

        var todo = await service.CreateTodoAsync("Köp mjölk");

        Assert.Equal("Köp mjölk", todo.Text);
        Assert.False(todo.Completed);
        Assert.NotEqual(default, todo.Id);
    }

    [Fact]
    public async Task QueryTodos_SearchMatchesCaseInsensitive()
    {
        var (service, store) = CreateServiceWithStore();
        await service.CreateTodoAsync("Köp mjölk");
        await service.CreateTodoAsync("Handla bröd");

        var results = await service.QueryTodosAsync("mjölk", TodoStatusFilter.All);

        Assert.Single(results);
        Assert.Contains(results, t => t.Text.Contains("mjölk", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task QueryTodos_FilterCompletedOnly()
    {
        var (service, store) = CreateServiceWithStore();
        var open = await service.CreateTodoAsync("Öppen todo");
        var done = await service.CreateTodoAsync("Klar todo");
        await store.ToggleAsync(done.Id);

        var results = await service.QueryTodosAsync(null, TodoStatusFilter.Completed);

        Assert.Single(results);
        Assert.Equal(done.Id, results[0].Id);
        Assert.All(results, t => Assert.True(t.Completed));
    }

    [Fact]
    public async Task QueryTodos_FilterOpenOnly()
    {
        var (service, store) = CreateServiceWithStore();
        var open = await service.CreateTodoAsync("Öppen todo");
        var done = await service.CreateTodoAsync("Klar todo");
        await store.ToggleAsync(done.Id);

        var results = await service.QueryTodosAsync(null, TodoStatusFilter.Open);

        Assert.Single(results);
        Assert.Equal(open.Id, results[0].Id);
        Assert.All(results, t => Assert.False(t.Completed));
    }

    [Fact]
    public async Task QueryTodos_SearchTooLong_Throws()
    {
        var service = CreateService();
        var longTerm = new string('x', 101);

        await Assert.ThrowsAsync<ArgumentException>(() => service.QueryTodosAsync(longTerm, TodoStatusFilter.All));
    }

    [Fact]
    public async Task QueryTodos_FilterByCategory()
    {
        var (service, store) = CreateServiceWithStore();
        await service.CreateTodoAsync("Hem todo", "Hem");
        await service.CreateTodoAsync("Jobb todo", "Jobb");

        var results = await service.QueryTodosAsync(null, TodoStatusFilter.All, "Jobb");

        Assert.Single(results);
        Assert.Equal("Jobb todo", results[0].Text);
        Assert.Equal("Jobb", results[0].Category);
    }

    [Fact]
    public async Task CreateTodo_WithEmptyTitle_Throws()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTodoAsync(string.Empty));
    }

    [Fact]
    public async Task CreateTodo_WithNullTitle_Throws()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTodoAsync(null!));
    }

    [Fact]
    public async Task CreateTodo_WithTooLongTitle_Throws()
    {
        var service = CreateService();
        var longTitle = new string('a', 201);

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTodoAsync(longTitle));
    }

    [Fact]
    public async Task CreateTodo_TrimsWhitespace()
    {
        var service = CreateService();

        var todo = await service.CreateTodoAsync("  Städning  ");

        Assert.Equal("Städning", todo.Text);
    }

    [Fact]
    public async Task CreateTodo_CollapsesInnerWhitespace()
    {
        var service = CreateService();

        var todo = await service.CreateTodoAsync("  Städa   och   handla  ");

        Assert.Equal("Städa och handla", todo.Text);
    }

    [Fact]
    public async Task CreateTodo_SanitizesXssAttempt()
    {
        var encoder = EncodingFactory.CreateHtmlEncoder();
        var service = CreateService(encoder);

        var todo = await service.CreateTodoAsync("<script>alert('xss')</script>");

        Assert.Equal(encoder.Encode("<script>alert('xss')</script>"), todo.Text);
        Assert.DoesNotContain("<script>", todo.Text);
    }

    [Fact]
    public async Task CreateTodo_DefaultsCategory()
    {
        var service = CreateService();

        var todo = await service.CreateTodoAsync("Test");

        Assert.Equal(TodoService.DefaultCategory, todo.Category);
    }

    [Fact]
    public async Task CreateTodo_CustomCategory_TrimAndSanitize()
    {
        var encoder = EncodingFactory.CreateHtmlEncoder();
        var service = CreateService(encoder);

        var todo = await service.CreateTodoAsync("Test", "  <Work>  ");

        Assert.Equal(encoder.Encode("<Work>"), todo.Category);
    }

    [Fact]
    public async Task CreateTodo_CategoryCollapsesWhitespace()
    {
        var service = CreateService();

        var todo = await service.CreateTodoAsync("Test", "  Team    Alpha  ");

        Assert.Equal("Team Alpha", todo.Category);
    }

    [Fact]
    public async Task ToggleTodo_TogglesState()
    {
        var (service, store) = CreateServiceWithStore();
        var todo = await service.CreateTodoAsync("Flip me");

        var toggled = await service.ToggleTodoAsync(todo.Id);

        Assert.NotNull(toggled);
        Assert.True(toggled!.Completed);

        var toggledBack = await service.ToggleTodoAsync(todo.Id);
        Assert.NotNull(toggledBack);
        Assert.False(toggledBack!.Completed);
    }

    [Fact]
    public async Task ToggleTodo_InvalidId_Throws()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ArgumentException>(() => service.ToggleTodoAsync(Guid.Empty));
    }

    private static TodoService CreateService(HtmlEncoder? encoder = null)
    {
        var env = new FakeWebHostEnvironment();
        return new TodoService(new TodoStore(env), encoder ?? EncodingFactory.CreateHtmlEncoder());
    }

    private static (TodoService service, TodoStore store) CreateServiceWithStore(HtmlEncoder? encoder = null)
    {
        var env = new FakeWebHostEnvironment();
        var store = new TodoStore(env);
        var service = new TodoService(store, encoder ?? EncodingFactory.CreateHtmlEncoder());
        return (service, store);
    }

    private sealed class FakeWebHostEnvironment : IWebHostEnvironment
    {
        public FakeWebHostEnvironment()
        {
            ContentRootPath = Path.Combine(Path.GetTempPath(), "todo-tests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(ContentRootPath);
            ContentRootFileProvider = new NullFileProvider();
            WebRootFileProvider = new NullFileProvider();
        }

        public string ApplicationName { get; set; } = "Tests";
        public IFileProvider WebRootFileProvider { get; set; }
        public string WebRootPath { get; set; } = string.Empty;
        public string EnvironmentName { get; set; } = "Development";
        public string ContentRootPath { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
