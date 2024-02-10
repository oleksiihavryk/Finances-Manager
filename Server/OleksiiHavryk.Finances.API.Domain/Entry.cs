namespace OleksiiHavryk.Finances.API.Domain;
/// <summary>
///     Domain model for debit and credit entries in the book record. 
/// </summary>
public class Entry
{
    public Guid Id { get; set; }
    public Guid RecordId { get; set; }
    public Guid CurrencyId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Count { get; set; }
    public EntryType Type { get; set; }
}