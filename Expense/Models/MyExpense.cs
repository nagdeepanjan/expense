using System.ComponentModel.DataAnnotations;

namespace Expense.Models;

/// <summary>
/// Represents an expense entry in the system
/// </summary>
public class MyExpense
{
    /// <summary>
    /// Unique identifier for the expense
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Description of the expense
    /// </summary>
    [Required(ErrorMessage = "Description is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 200 characters")]
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Amount spent
    /// </summary>
    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, 999999.99, ErrorMessage = "Amount must be between $0.01 and $999,999.99")]
    [Display(Name = "Amount")]
    [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
    public decimal Amount { get; set; }

    /// <summary>
    /// Category of the expense
    /// </summary>
    [Required(ErrorMessage = "Category is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Category must be between 2 and 50 characters")]
    [Display(Name = "Category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Date when the expense occurred
    /// </summary>
    [Required(ErrorMessage = "Date is required")]
    [Display(Name = "Date")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Today;

    /// <summary>
    /// Gets the formatted amount as currency string
    /// </summary>
    public string FormattedAmount => Amount.ToString("C");

    /// <summary>
    /// Gets the formatted date as short date string
    /// </summary>
    public string FormattedDate => Date.ToString("yyyy-MM-dd");
}