using Microsoft.EntityFrameworkCore;
using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Data;
/// <summary>
///     All resources database context class.
/// </summary>
public class ResourcesDbContext : DbContext
{
    public DbSet<Record> Records { get; set; }
    public DbSet<Entry> Entries { get; set; }

    public ResourcesDbContext(DbContextOptions<ResourcesDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Entry>()
            .Property(c => c.ItemPrice)
            .HasPrecision(
                precision: 25,
                scale: 10);
    }
}