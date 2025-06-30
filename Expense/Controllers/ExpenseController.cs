using Expense.Data.Service;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;

namespace Expense.Controllers;

/// <summary>
/// Controller for managing expenses
/// </summary>
public class ExpenseController : Controller
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
    }

    /// <summary>
    /// Display all expenses with optional filtering
    /// </summary>
    public async Task<IActionResult> Index(string? category, DateTime? startDate, DateTime? endDate)
    {
        try
        {
            var expenses = await _expenseService.GetAllAsync(category, startDate, endDate);
            
            // Pass filter values to view for maintaining state
            ViewBag.Category = category;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.Categories = await _expenseService.GetCategoriesAsync();
            ViewBag.Total = await _expenseService.GetTotalAsync(startDate, endDate);
            
            return View(expenses);
        }
        catch (Exception ex)
        {
            // Log error here in a real application
            TempData["Error"] = "An error occurred while loading expenses.";
            return View(new List<MyExpense>());
        }
    }

    /// <summary>
    /// Display create expense form
    /// </summary>
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _expenseService.GetCategoriesAsync();
        return View();
    }

    /// <summary>
    /// Handle expense creation
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MyExpense expense)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _expenseService.AddAsync(expense);
                TempData["Success"] = "Expense added successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log error here in a real application
                ModelState.AddModelError("", "An error occurred while saving the expense.");
            }
        }

        ViewBag.Categories = await _expenseService.GetCategoriesAsync();
        return View(expense);
    }

    /// <summary>
    /// Display edit expense form
    /// </summary>
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var expense = await _expenseService.GetByIdAsync(id);
            if (expense == null)
            {
                TempData["Error"] = "Expense not found.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _expenseService.GetCategoriesAsync();
            return View(expense);
        }
        catch (Exception ex)
        {
            // Log error here in a real application
            TempData["Error"] = "An error occurred while loading the expense.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// Handle expense update
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MyExpense expense)
    {
        if (id != expense.Id)
        {
            TempData["Error"] = "Invalid expense ID.";
            return RedirectToAction(nameof(Index));
        }

        if (ModelState.IsValid)
        {
            try
            {
                var success = await _expenseService.UpdateAsync(expense);
                if (success)
                {
                    TempData["Success"] = "Expense updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Expense not found.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log error here in a real application
                ModelState.AddModelError("", "An error occurred while updating the expense.");
            }
        }

        ViewBag.Categories = await _expenseService.GetCategoriesAsync();
        return View(expense);
    }

    /// <summary>
    /// Display delete confirmation
    /// </summary>
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var expense = await _expenseService.GetByIdAsync(id);
            if (expense == null)
            {
                TempData["Error"] = "Expense not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(expense);
        }
        catch (Exception ex)
        {
            // Log error here in a real application
            TempData["Error"] = "An error occurred while loading the expense.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// Handle expense deletion
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var success = await _expenseService.DeleteAsync(id);
            if (success)
            {
                TempData["Success"] = "Expense deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Expense not found.";
            }
        }
        catch (Exception ex)
        {
            // Log error here in a real application
            TempData["Error"] = "An error occurred while deleting the expense.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Get chart data for expense visualization
    /// </summary>
    public async Task<IActionResult> GetChart()
    {
        try
        {
            var data = await _expenseService.GetChartDataAsync();
            return Json(data);
        }
        catch (Exception ex)
        {
            // Log error here in a real application
            return Json(new { error = "Failed to load chart data" });
        }
    }
}