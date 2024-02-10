using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Data.Interfaces;

/// <summary>
///     Repository of a entry domain model.
/// </summary>
public interface IEntryRepository : IRepository<Entry, Guid>
{
}