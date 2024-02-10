using Microsoft.EntityFrameworkCore;
using OleksiiHavryk.Finances.API.Data.Exceptions;
using OleksiiHavryk.Finances.API.Data.Extensions;
using OleksiiHavryk.Finances.API.Data.Interfaces;
using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Data;

/// <summary>
///     Repository class for updated entities in database.
/// </summary>
public class RecordRepository : IRecordRepository
{
    private readonly ResourcesDbContext _dbContext;

    public RecordRepository(ResourcesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Record>> GetAllAsync()
        => await _dbContext.GetFullRecords().ToArrayAsync();

    public async Task<Record> SaveAsync(Record saved)
    {
        var entry = _dbContext.Records.Add(saved);
        await _dbContext.SaveChangesAsync();
        return (await _dbContext.GetFullRecords()
            .FirstOrDefaultAsync(v => v.Id == entry.Entity.Id))
            ?? throw new ApplicationException();
    }
    public async Task<Record> UpdateAsync(Record updated)
    {
        var entry = _dbContext.Records.Update(updated);
        await _dbContext.SaveChangesAsync();
        return (await _dbContext.GetFullRecords()
                   .FirstOrDefaultAsync(v => v.Id == entry.Entity.Id))
               ?? throw new ApplicationException();
    }
    public async Task DeleteAsync(Guid key)
    {
        var entity = await _dbContext.GetFullRecords()
            .FirstOrDefaultAsync(r => r.Id == key);

        if (entity == null) throw new IdentifierNotFoundException();

        _dbContext.Records.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}