using MailingListManager.Data;
using MailingListManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MailingListManager.Services;

public class SubscriberService
{
    private readonly MailingListContext _context;

    public SubscriberService(MailingListContext context)
    {
        _context = context;
    }

    public async Task<List<Subscriber>> GetAllAsync()
    {
        return await _context.Subscribers
            .Where(s => s.Email != null)
            .OrderBy(s => s.Email)
            .ToListAsync();
    }
}
