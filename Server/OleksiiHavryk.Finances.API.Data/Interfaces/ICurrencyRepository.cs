using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Data.Interfaces;

/// <summary>
///     Repository of a currency domain model.
/// </summary>
public interface ICurrencyRepository : IRepository<Currency, Guid>
{
    public Task<IEnumerable<Currency>> GetAllAsync();
}