using System.ComponentModel.DataAnnotations;
using OleksiiHavryk.Finances.API.Domain;

namespace OleksiiHavryk.Finances.API.DTO;

/// <summary>
///     DTO of entry domain model.
/// </summary>
public class EntryDto
{
    [Required] public Guid Id { get; set; }
    [Required] public Guid RecordId { get; set; }
    [Required] public int CurrencyId { get; set; }
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = null!;
    [Required] public decimal Count { get; set; }
    [Required] public EntryType Type { get; set; }
}