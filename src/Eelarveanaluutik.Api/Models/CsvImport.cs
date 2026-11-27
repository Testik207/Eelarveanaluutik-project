namespace Eelarveanaluutik.Api.Models;

public class CsvImport
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
    public string OriginalFileName { get; set; } = string.Empty;
    public ImportStatus Status { get; set; } = ImportStatus.Pending;
    public int TotalRows { get; set; }
    public int ProcessedRows { get; set; }
    public int FailedRows { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAtUtc { get; set; }
    public ICollection<CsvImportRow> Rows { get; set; } = new List<CsvImportRow>();
}

public enum ImportStatus { Pending, Processing, Completed, Failed }
