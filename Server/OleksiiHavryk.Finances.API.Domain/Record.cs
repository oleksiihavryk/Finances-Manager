namespace OleksiiHavryk.Finances.API.Domain;
/// <summary>
///     Domain model for record entries in the book. 
/// </summary>
public class Record
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public List<Entry> Entries { get; set; } = null!;
}