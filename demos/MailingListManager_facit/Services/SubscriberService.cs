using MailingListManager.Models;
using System.Text.RegularExpressions;

namespace MailingListManager.Services;

/// <summary>
/// Subscriber management service with in-memory storage (MVP)
/// Follows: SRP (only subscriber CRUD), DRY (reusable validation), KISS (simple in-memory)
///
/// NOTE: Uses in-memory list for demo. Production should use EF Core + database.
/// </summary>
public class SubscriberService : ISubscriberService
{
    // In-memory storage for demo - production uses database
    private readonly List<Subscriber> _subscribers = new();
    private int _nextId = 1;

    // Email validation regex (RFC 5322 simplified)
    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled
    );

    public SubscriberService()
    {
        // Seed a couple of subscribers so list-vyn inte är tom i demon
        _subscribers.AddRange(new[]
        {
            new Subscriber { Id = _nextId++, Email = "welcome@example.com", CreatedAt = DateTime.UtcNow.AddDays(-2), IsActive = true },
            new Subscriber { Id = _nextId++, Email = "ai-demo@example.com", CreatedAt = DateTime.UtcNow.AddDays(-1), IsActive = true }
        });
    }

    public async Task<List<Subscriber>> GetAllAsync()
    {
        await Task.Delay(50); // Simulate async DB call
        return _subscribers.Where(s => s.IsActive).ToList();
    }

    public async Task<Subscriber?> AddSubscriberAsync(string email)
    {
        // Validation: null/empty
        if (string.IsNullOrWhiteSpace(email))
            return null;

        // Sanitize: trim and lowercase
        email = email.Trim().ToLower();

        // Validation: email format
        if (!EmailRegex.IsMatch(email))
            return null;

        // Validation: duplicate check
        if (await EmailExistsAsync(email))
            return null;

        // Create subscriber
        var subscriber = new Subscriber
        {
            Id = _nextId++,
            Email = email,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _subscribers.Add(subscriber);
        await Task.Delay(50); // Simulate async DB save

        return subscriber;
    }

    public async Task<bool> RemoveSubscriberAsync(int id)
    {
        await Task.Delay(50); // Simulate async DB call

        var subscriber = _subscribers.FirstOrDefault(s => s.Id == id && s.IsActive);

        if (subscriber == null)
            return false;

        // Soft delete (set IsActive = false)
        subscriber.IsActive = false;
        return true;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        email = email.Trim().ToLower();

        await Task.Delay(20); // Simulate async DB call
        return _subscribers.Any(s => s.IsActive && s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }
}
