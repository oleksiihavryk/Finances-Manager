using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Data.Interfaces;

/// <summary>
///     Repository of a updated domain model.
/// </summary>
public interface IRecordRepository : IRepository<Record, Guid>
{
    public Task<IEnumerable<Record>> GetAllAsync();
    public Task<Record> UpdateAsync(Record updated);
    public Task<Record> SaveAsync(Record saved);
    public Task DeleteAsync(Guid key);
}