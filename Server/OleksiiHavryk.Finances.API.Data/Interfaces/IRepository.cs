namespace OleksiiHavryk.Finances.API.Data.Interfaces;
/// <summary>
///     Repository interface for generic type of entity and their key.
/// </summary>
/// <typeparam name="TEntity">
///     Entity type of a domain model which has stored inside a database.
/// </typeparam>
/// <typeparam name="TKey">
///     Type of entity identifier.
/// </typeparam>
public interface IRepository<TEntity, TKey>
{
    public Task<TEntity> SaveAsync(TEntity saved);
    public Task DeleteAsync(TKey key);
}