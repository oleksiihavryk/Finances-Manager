using Microsoft.EntityFrameworkCore;
using OleksiiHavryk.Finances.API.Data.Exceptions;
using OleksiiHavryk.Finances.API.Data.Interfaces;
using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Data;

/// <summary>
///     Repository class for currency entities in database.
/// </summary>
public class CurrencyRepository : ICurrencyRepository
{
    private readonly ResourcesDbContext _dbContext;

    public CurrencyRepository(ResourcesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Currency>> GetAllAsync()
        => await _dbContext.Currencies.ToArrayAsync();
    public async Task<Currency> SaveAsync(Currency saved)
    {
        var entry = _dbContext.Currencies.Add(saved);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task DeleteAsync(Guid key)
    {
        var entity = await _dbContext.Currencies
            .FirstOrDefaultAsync(r => r.Id == key);

        if (entity == null) throw new IdentifierNotFoundException();

        _dbContext.Currencies.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}