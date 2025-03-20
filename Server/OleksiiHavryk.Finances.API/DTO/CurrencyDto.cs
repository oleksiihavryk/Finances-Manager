using System.ComponentModel.DataAnnotations;

namespace OleksiiHavryk.Finances.API.DTO;

/// <summary>
///     DTO of currency domain model.
/// </summary>
public class CurrencyDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = null!;
    [Required]
    public decimal Value { get; set; }
    [Required]
    public char Symbol { get; set; }
}