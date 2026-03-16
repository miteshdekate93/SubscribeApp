using Microsoft.EntityFrameworkCore;
using SubscribeApp.Models;

namespace SubscribeApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Subscriber> Subscribers => Set<Subscriber>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.HasIndex(s => s.Email).IsUnique();
            entity.Property(s => s.Name).IsRequired().HasMaxLength(200);
            entity.Property(s => s.Email).IsRequired().HasMaxLength(320);
            entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });
    }
}
