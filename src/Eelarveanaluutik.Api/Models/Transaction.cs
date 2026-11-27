namespace Eelarveanaluutik.Api.Models;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid? CsvImportRowId { get; set; }
    public CsvImportRow? CsvImportRow { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

public enum TransactionType { Income, Expense }
