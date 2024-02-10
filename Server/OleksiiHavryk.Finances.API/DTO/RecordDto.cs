using System.ComponentModel.DataAnnotations;

namespace OleksiiHavryk.Finances.API.DTO;

/// <summary>
///     DTO of record domain model.
/// </summary>
public class RecordDto
{
    [Required] public Guid Id { get; set; }
    [Required]
    [MaxLength(128)]
    public string Title { get; set; } = null!;
    public List<EntryDto> Entries { get; set; } = null!;
}