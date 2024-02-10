using Microsoft.EntityFrameworkCore;
using OleksiiHavryk.Finances.API.Data.Exceptions;
using OleksiiHavryk.Finances.API.Data.Interfaces;
using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Data;

public class EntryRepository : IEntryRepository
{
    private readonly ResourcesDbContext _dbContext;

    public EntryRepository(ResourcesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Entry> SaveAsync(Entry saved)
    {
        var entry = _dbContext.Entries.Add(saved);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task DeleteAsync(Guid key)
    {
        var entity = await _dbContext.Entries
            .FirstOrDefaultAsync(r => r.Id == key);

        if (entity == null) throw new IdentifierNotFoundException();

        _dbContext.Entries.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}