namespace Eelarveanaluutik.Api.Models;

public class CsvImportRow
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid CsvImportId { get; set; }
    public CsvImport CsvImport { get; set; } = null!;
    public int RowNumber { get; set; }
    public string RawData { get; set; } = string.Empty;
    public ImportRowStatus Status { get; set; } = ImportRowStatus.Pending;
    public string? ErrorMessage { get; set; }
}

public enum ImportRowStatus { Pending, Completed, Failed }
