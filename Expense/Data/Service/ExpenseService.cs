using Expense.Models;
using Microsoft.EntityFrameworkCore;

namespace Expense.Data.Service;

/// <summary>
/// Service implementation for expense operations
/// </summary>
public class ExpenseService : IExpenseService
{
    private readonly ExpenseDbContext _context;

    public ExpenseService(ExpenseDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<MyExpense>> GetAllAsync(string? category = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Expenses.AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(e => e.Category.ToLower().Contains(category.ToLower()));
        }

        if (startDate.HasValue)
        {
            query = query.Where(e => e.Date >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(e => e.Date <= endDate.Value);
        }

        return await query
            .OrderByDescending(e => e.Date)
            .ThenByDescending(e => e.Id)
            .ToListAsync();
    }

    public async Task<MyExpense?> GetByIdAsync(int id)
    {
        return await _context.Expenses.FindAsync(id);
    }

    public async Task<MyExpense> AddAsync(MyExpense expense)
    {
        if (expense == null)
            throw new ArgumentNullException(nameof(expense));

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<bool> UpdateAsync(MyExpense expense)
    {
        if (expense == null)
            throw new ArgumentNullException(nameof(expense));

        var existingExpense = await _context.Expenses.FindAsync(expense.Id);
        if (existingExpense == null)
            return false;

        existingExpense.Description = expense.Description;
        existingExpense.Amount = expense.Amount;
        existingExpense.Category = expense.Category;
        existingExpense.Date = expense.Date;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
            return false;

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<object>> GetChartDataAsync()
    {
        return await _context.Expenses
            .GroupBy(e => e.Category)
            .Select(g => new { 
                Category = g.Key, 
                Total = g.Sum(e => e.Amount) 
            })
            .OrderByDescending(x => x.Total)
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
        return await _context.Expenses
            .Select(e => e.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Expenses.AsQueryable();

        if (startDate.HasValue)
        {
            query = query.Where(e => e.Date >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(e => e.Date <= endDate.Value);
        }

        return await query.SumAsync(e => e.Amount);
    }
}