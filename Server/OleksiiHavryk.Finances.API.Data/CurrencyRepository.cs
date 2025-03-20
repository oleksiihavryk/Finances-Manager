using System.Collections;
using Microsoft.EntityFrameworkCore;
using OleksiiHavryk.Finances.API.Core;
using OleksiiHavryk.Finances.API.Data.Exceptions;
using OleksiiHavryk.Finances.API.Data.Interfaces;
using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Data;

/// <summary>
///     Repository class for currency entities in database.
/// </summary>
public class CurrencyRepository : ICurrencyRepository
{
    private readonly ICurrencyScraper _scraper;

    public CurrencyRepository(ICurrencyScraper scraper)
    {
        _scraper = scraper;
    }

    public async Task<IEnumerable<Currency>> GetAllAsync()
        => await _scraper.ScrapAsync();
}