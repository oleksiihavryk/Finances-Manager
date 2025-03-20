using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Core;
/// <summary>
///     Interface of the currency scrapper feature.
/// </summary>
public interface ICurrencyScraper
{
    Task<IEnumerable<Currency>> ScrapAsync();
}