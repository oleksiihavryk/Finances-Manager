namespace OleksiiHavryk.Finances.API.Domain;
/// <summary>
///     Domain model for currency in the system. 
/// </summary>
public class Currency
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Value { get; set; }
    public char Symbol { get; set; }
}