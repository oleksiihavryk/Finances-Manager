using Microsoft.EntityFrameworkCore;
using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Data.Extensions;
/// <summary>
///     Extensions for ResourcesDbContext class.
/// </summary>
internal static class ResourcesDbContextExtensions
{
    public static IQueryable<Record> GetFullRecords(this ResourcesDbContext dbContext)
        => dbContext.Records.Include(r => r.Entries);
}