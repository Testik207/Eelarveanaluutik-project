namespace Eelarveanaluutik.Api.Models;

public class MonthlyBudget
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
    public DateOnly Month { get; set; }
    public decimal Amount { get; set; }
}
