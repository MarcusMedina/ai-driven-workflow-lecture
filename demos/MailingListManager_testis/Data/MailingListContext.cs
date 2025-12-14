using MailingListManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MailingListManager.Data;

public class MailingListContext : DbContext
{
    public MailingListContext(DbContextOptions<MailingListContext> options) : base(options)
    {
    }

    public DbSet<Subscriber> Subscribers => Set<Subscriber>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Subscriber configuration
        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(254);
            
            entity.HasIndex(e => e.Email)
                .IsUnique();
            
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}
