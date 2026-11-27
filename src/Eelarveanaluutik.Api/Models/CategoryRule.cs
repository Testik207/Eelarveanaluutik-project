namespace Eelarveanaluutik.Api.Models;

public class CategoryRule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public string Keyword { get; set; } = string.Empty;
    public int Priority { get; set; }
}
