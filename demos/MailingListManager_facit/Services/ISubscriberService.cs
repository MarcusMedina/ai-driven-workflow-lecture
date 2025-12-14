using MailingListManager.Models;

namespace MailingListManager.Services;

/// <summary>
/// Subscriber management service interface
/// Follows: SRP (only manages subscribers), SoC (separated from auth and UI)
/// </summary>
public interface ISubscriberService
{
    /// <summary>
    /// Gets all active subscribers
    /// </summary>
    Task<List<Subscriber>> GetAllAsync();

    /// <summary>
    /// Adds a new subscriber to the mailing list
    /// </summary>
    /// <param name="email">Email address to add</param>
    /// <returns>Added subscriber or null if validation fails</returns>
    Task<Subscriber?> AddSubscriberAsync(string email);

    /// <summary>
    /// Removes a subscriber from the mailing list
    /// </summary>
    /// <param name="id">Subscriber ID to remove</param>
    /// <returns>True if removed, false if not found</returns>
    Task<bool> RemoveSubscriberAsync(int id);

    /// <summary>
    /// Checks if email already exists in the list
    /// </summary>
    Task<bool> EmailExistsAsync(string email);
}
