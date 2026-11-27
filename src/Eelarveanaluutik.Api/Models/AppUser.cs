namespace Eelarveanaluutik.Api.Models;

public class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = string.Empty;
    public string NormalizedEmail { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

public enum UserRole { User, Admin, Service }
