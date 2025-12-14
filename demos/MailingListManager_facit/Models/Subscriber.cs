namespace MailingListManager.Models;

/// <summary>
/// Represents an email subscriber in the mailing list
/// </summary>
public class Subscriber
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
