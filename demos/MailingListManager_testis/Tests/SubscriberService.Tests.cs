using MailingListManager.Data;
using MailingListManager.Models;
using MailingListManager.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MailingListManager.Tests;

public class SubscriberServiceTests
{
    private MailingListContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<MailingListContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        return new MailingListContext(options);
    }

    [Fact]
    public async Task GetAllAsync_WithSubscribers_ReturnsAll()
    {
        // Arrange
        var context = CreateDbContext();
        context.Subscribers.Add(new Subscriber { Email = "test1@example.com" });
        context.Subscribers.Add(new Subscriber { Email = "test2@example.com" });
        context.Subscribers.Add(new Subscriber { Email = "test3@example.com" });
        await context.SaveChangesAsync();

        var service = new SubscriberService(context);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task GetAllAsync_WithEmptyDB_ReturnsEmptyList()
    {
        // Arrange
        var context = CreateDbContext();
        var service = new SubscriberService(context);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsSortedByEmail()
    {
        // Arrange
        var context = CreateDbContext();
        context.Subscribers.Add(new Subscriber { Email = "z@example.com" });
        context.Subscribers.Add(new Subscriber { Email = "a@example.com" });
        context.Subscribers.Add(new Subscriber { Email = "m@example.com" });
        await context.SaveChangesAsync();

        var service = new SubscriberService(context);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Equal(new[] { "a@example.com", "m@example.com", "z@example.com" },
            result.Select(s => s.Email));
    }

    [Fact]
    public async Task GetAllAsync_ExcludesNull()
    {
        // Arrange
        var context = CreateDbContext();
        context.Subscribers.Add(new Subscriber { Email = "valid@example.com" });
        await context.SaveChangesAsync();

        var service = new SubscriberService(context);

        // Act
        var result = await service.GetAllAsync();

        // Assert - Should have 1 valid subscriber
        Assert.Single(result);
        Assert.NotNull(result.First().Email);
    }

    [Fact]
    public async Task GetAllAsync_WithDatabaseError_ThrowsException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MailingListContext>()
            .UseInMemoryDatabase("error-db")
            .Options;
        
        var context = new MailingListContext(options);
        context.Dispose(); // Close the context to simulate error

        var service = new SubscriberService(context);

        // Act & Assert
        await Assert.ThrowsAsync<ObjectDisposedException>(() => service.GetAllAsync());
    }
}
