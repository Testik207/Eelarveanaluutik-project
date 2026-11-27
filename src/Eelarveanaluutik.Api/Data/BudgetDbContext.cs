using Eelarveanaluutik.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Eelarveanaluutik.Api.Data;

public class BudgetDbContext(DbContextOptions<BudgetDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<MonthlyBudget> MonthlyBudgets => Set<MonthlyBudget>();
    public DbSet<CategoryRule> CategoryRules => Set<CategoryRule>();
    public DbSet<CsvImport> CsvImports => Set<CsvImport>();
    public DbSet<CsvImportRow> CsvImportRows => Set<CsvImportRow>();
    public DbSet<MonthlyReport> MonthlyReports => Set<MonthlyReport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var users = modelBuilder.Entity<AppUser>();
        users.Property(x => x.Email).HasMaxLength(254);
        users.Property(x => x.NormalizedEmail).HasMaxLength(254);
        users.HasIndex(x => x.NormalizedEmail).IsUnique();
        users.Property(x => x.DisplayName).HasMaxLength(100);
        users.Property(x => x.PasswordHash).HasMaxLength(500);
        users.Property(x => x.Role).HasConversion<string>().HasMaxLength(20);
        users.ToTable(t => t.HasCheckConstraint("CK_Users_Role", "\"Role\" IN ('User', 'Admin', 'Service')"));

        var categories = modelBuilder.Entity<Category>();
        categories.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        categories.Property(x => x.Name).HasMaxLength(80);
        categories.Property(x => x.NormalizedName).HasMaxLength(80);
        categories.HasIndex(x => new { x.UserId, x.NormalizedName }).IsUnique();
        categories.HasAlternateKey(x => new { x.Id, x.UserId });

        var transactions = modelBuilder.Entity<Transaction>();
        transactions.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        transactions.HasOne(x => x.Category).WithMany()
            .HasForeignKey(x => new { x.CategoryId, x.UserId })
            .HasPrincipalKey(x => new { x.Id, x.UserId }).OnDelete(DeleteBehavior.Restrict);
        transactions.HasOne(x => x.CsvImportRow).WithMany()
            .HasForeignKey(x => new { x.CsvImportRowId, x.UserId })
            .HasPrincipalKey(x => new { x.Id, x.UserId }).OnDelete(DeleteBehavior.Restrict);
        transactions.HasIndex(x => x.CsvImportRowId).IsUnique();
        transactions.HasIndex(x => new { x.UserId, x.Date });
        transactions.Property(x => x.Amount).HasPrecision(18, 2);
        transactions.Property(x => x.Description).HasMaxLength(500);
        transactions.Property(x => x.Type).HasConversion<string>().HasMaxLength(20);
        transactions.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Transactions_Amount", "\"Amount\" > 0");
            t.HasCheckConstraint("CK_Transactions_Type", "\"Type\" IN ('Income', 'Expense')");
        });

        var budgets = modelBuilder.Entity<MonthlyBudget>();
        budgets.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        budgets.HasIndex(x => new { x.UserId, x.Month }).IsUnique();
        budgets.Property(x => x.Amount).HasPrecision(18, 2);
        budgets.ToTable(t =>
        {
            t.HasCheckConstraint("CK_MonthlyBudgets_Amount", "\"Amount\" >= 0");
            t.HasCheckConstraint("CK_MonthlyBudgets_Month", "EXTRACT(DAY FROM \"Month\") = 1");
        });

        var rules = modelBuilder.Entity<CategoryRule>();
        rules.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        rules.HasOne(x => x.Category).WithMany()
            .HasForeignKey(x => new { x.CategoryId, x.UserId })
            .HasPrincipalKey(x => new { x.Id, x.UserId }).OnDelete(DeleteBehavior.Restrict);
        rules.Property(x => x.Keyword).HasMaxLength(120);
        rules.HasIndex(x => new { x.UserId, x.Priority });

        var imports = modelBuilder.Entity<CsvImport>();
        imports.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        imports.HasAlternateKey(x => new { x.Id, x.UserId });
        imports.Property(x => x.OriginalFileName).HasMaxLength(255);
        imports.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        imports.HasIndex(x => new { x.Status, x.CreatedAtUtc });
        imports.ToTable(t =>
        {
            t.HasCheckConstraint("CK_CsvImports_Counts", "\"TotalRows\" >= 0 AND \"ProcessedRows\" >= 0 AND \"ProcessedRows\" <= \"TotalRows\" AND \"FailedRows\" >= 0 AND \"FailedRows\" <= \"ProcessedRows\"");
            t.HasCheckConstraint("CK_CsvImports_Status", "\"Status\" IN ('Pending', 'Processing', 'Completed', 'Failed')");
        });

        var rows = modelBuilder.Entity<CsvImportRow>();
        rows.HasOne(x => x.CsvImport).WithMany(x => x.Rows)
            .HasForeignKey(x => new { x.CsvImportId, x.UserId })
            .HasPrincipalKey(x => new { x.Id, x.UserId }).OnDelete(DeleteBehavior.Cascade);
        rows.HasAlternateKey(x => new { x.Id, x.UserId });
        rows.HasIndex(x => new { x.CsvImportId, x.RowNumber }).IsUnique();
        rows.Property(x => x.RawData).HasMaxLength(10000);
        rows.Property(x => x.ErrorMessage).HasMaxLength(1000);
        rows.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        rows.ToTable(t =>
        {
            t.HasCheckConstraint("CK_CsvImportRows_RowNumber", "\"RowNumber\" > 0");
            t.HasCheckConstraint("CK_CsvImportRows_Status", "\"Status\" IN ('Pending', 'Completed', 'Failed')");
        });

        var reports = modelBuilder.Entity<MonthlyReport>();
        reports.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        reports.HasIndex(x => new { x.UserId, x.Month }).IsUnique();
        reports.Property(x => x.TotalIncome).HasPrecision(18, 2);
        reports.Property(x => x.TotalExpenses).HasPrecision(18, 2);
        reports.ToTable(t =>
        {
            t.HasCheckConstraint("CK_MonthlyReports_Totals", "\"TotalIncome\" >= 0 AND \"TotalExpenses\" >= 0");
            t.HasCheckConstraint("CK_MonthlyReports_Month", "EXTRACT(DAY FROM \"Month\") = 1");
        });
    }
}
