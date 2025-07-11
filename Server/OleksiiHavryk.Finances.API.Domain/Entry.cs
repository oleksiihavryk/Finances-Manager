﻿namespace OleksiiHavryk.Finances.API.Domain;
/// <summary>
///     Domain model for debit and credit entries in the book record. 
/// </summary>
public class Entry
{
    public Guid Id { get; set; }
    public Guid RecordId { get; set; }
    public int CurrencyId { get; set; }
    public string Name { get; set; } = null!;
    public int Count { get; set; }
    public decimal ItemPrice { get; set; }
    public EntryType Type { get; set; }
}