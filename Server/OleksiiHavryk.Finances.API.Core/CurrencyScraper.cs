using System.Globalization;
using System.Text.RegularExpressions;
using OleksiiHavryk.Finances.API.Core.Constants;
using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.Core;
/// <summary>
///     Class which allow user to scrap all currencies used by application.
/// </summary>
public class CurrencyScraper : ICurrencyScraper
{
    private readonly HttpClient _client;

    public CurrencyScraper(IHttpClientFactory factory)
    {
        _client = factory.CreateClient(CurrencyScrapperConstants.HttpClientName);
    }

    public virtual async Task<IEnumerable<Currency>> ScrapAsync()
    {
        var response = await _client.GetAsync(
            requestUri: "https://bank.gov.ua/ua/markets/exchangerates");
        
        var html = await response.Content.ReadAsStringAsync();

        Currency hryvnya = new()
        {
            Id = 980,
            Name = "UAH",
            Symbol = '₴',
            Value = 1
        };
        Currency dollar = ScrapDollar(html);
        Currency euro = ScrapEuro(html);
        
        return [hryvnya, dollar, euro];
    }

    private Currency ScrapDollar(string html)
    {
        var val = Regex.Match(html,
                "(?<=<td class=\"hidden-sm\" data-label=\"Код цифровий\"><span class=\"value\">840<\\/span><\\/td>\n                <td data-label=\"Код літерний\">USD<\\/td>\n                <td data-label=\"Кількість одиниць валюти\">1<\\/td>\n                <td class=\"value-name\" data-label=\"Назва валюти\">\n                  <a href=\"\\/\\/bank\\.gov\\.ua\\/ua\\/markets\\/exchangerate-chart\\?cn%5B%5D=USD\">\n                     Долар США                   <\\/a>\n                <\\/td>\n                <td data-label=\"Офіційний курс\">)\\S*(?=<\\/td>\n              <\\/tr>)")
            .Value;
        
        return new()
        {
            Id = 840,
            Name = "USD",
            Symbol = '$',
            Value = Convert.ToDecimal(
                val.Replace(',', '.'), 
                CultureInfo.GetCultureInfo("en-US"))
        };
    }

    private Currency ScrapEuro(string html)
    {
        var val = Regex.Match(html,
                "(?<=<tr>\n                <td class=\"hidden-sm\" data-label=\"Код цифровий\"><span class=\"value\">978<\\/span><\\/td>\n                <td data-label=\"Код літерний\">EUR<\\/td>\n                <td data-label=\"Кількість одиниць валюти\">1<\\/td>\n                <td class=\"value-name\" data-label=\"Назва валюти\">\n                  <a href=\"\\/\\/bank\\.gov\\.ua\\/ua\\/markets\\/exchangerate-chart\\?cn%5B%5D=EUR\">\n                     Євро                   <\\/a>\n                <\\/td>\n                <td data-label=\"Офіційний курс\">)\\S*(?=<\\/td>\n              <\\/tr>)")
            .Value;
        
        return new()
        {
            Id = 978,
            Name = "EUR",
            Symbol = '\u20ac',
            Value = Convert.ToDecimal(
                val.Replace(',', '.'), 
                CultureInfo.GetCultureInfo("en-US"))
        };
    } 
}