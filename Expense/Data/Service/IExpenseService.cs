using Expense.Models;

namespace Expense.Data.Service;

/// <summary>
/// Service interface for expense operations
/// </summary>
public interface IExpenseService
{
    /// <summary>
    /// Gets all expenses with optional filtering
    /// </summary>
    /// <param name="category">Optional category filter</param>
    /// <param name="startDate">Optional start date filter</param>
    /// <param name="endDate">Optional end date filter</param>
    /// <returns>Collection of expenses</returns>
    Task<IEnumerable<MyExpense>> GetAllAsync(string? category = null, DateTime? startDate = null, DateTime? endDate = null);

    /// <summary>
    /// Gets an expense by ID
    /// </summary>
    /// <param name="id">Expense ID</param>
    /// <returns>Expense if found, null otherwise</returns>
    Task<MyExpense?> GetByIdAsync(int id);

    /// <summary>
    /// Adds a new expense
    /// </summary>
    /// <param name="expense">Expense to add</param>
    /// <returns>The added expense with generated ID</returns>
    Task<MyExpense> AddAsync(MyExpense expense);

    /// <summary>
    /// Updates an existing expense
    /// </summary>
    /// <param name="expense">Expense to update</param>
    /// <returns>True if updated successfully, false if not found</returns>
    Task<bool> UpdateAsync(MyExpense expense);

    /// <summary>
    /// Deletes an expense by ID
    /// </summary>
    /// <param name="id">Expense ID to delete</param>
    /// <returns>True if deleted successfully, false if not found</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Gets chart data grouped by category
    /// </summary>
    /// <returns>Chart data with categories and totals</returns>
    Task<IEnumerable<object>> GetChartDataAsync();

    /// <summary>
    /// Gets all unique categories
    /// </summary>
    /// <returns>List of unique categories</returns>
    Task<IEnumerable<string>> GetCategoriesAsync();

    /// <summary>
    /// Gets total expenses for a date range
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>Total amount</returns>
    Task<decimal> GetTotalAsync(DateTime? startDate = null, DateTime? endDate = null);
}