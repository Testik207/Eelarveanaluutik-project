namespace Eelarveanaluutik.Api.Models;

public class MonthlyReport
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
    public DateOnly Month { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
