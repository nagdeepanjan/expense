using Expense.Models;
using Microsoft.EntityFrameworkCore;

namespace Expense.Data;

/// <summary>
/// Database context for the Expense application
/// </summary>
public class ExpenseDbContext : DbContext
{
    public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the expenses table
    /// </summary>
    public DbSet<MyExpense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure MyExpense entity
        modelBuilder.Entity<MyExpense>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            entity.Property(e => e.Date)
                .IsRequired();
            
            // Add index for better query performance
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.Date);
        });
    }
}