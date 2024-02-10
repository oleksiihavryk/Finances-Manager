using Microsoft.EntityFrameworkCore;
using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Data;
/// <summary>
///     All resources database context class.
/// </summary>
public class ResourcesDbContext : DbContext
{
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<Entry> Entries { get; set; }

    public ResourcesDbContext(DbContextOptions<ResourcesDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Entry>()
            .HasOne<Currency>()
            .WithMany()
            .HasForeignKey(e => e.CurrencyId);

        modelBuilder.Entity<Currency>()
            .Property(c => c.Value)
            .HasPrecision(
                precision: 20,
                scale: 10);
        modelBuilder.Entity<Entry>()
            .Property(c => c.Count)
            .HasPrecision(
                precision: 25,
                scale: 10);
    }
}