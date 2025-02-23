﻿using System.ComponentModel.DataAnnotations;

namespace Expense.Models;

public class MyExpense
{
    public int Id { get; set; }

    [Required] public string Description { get; set; } = null!;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount needs to be in range")]
    public double Balance { get; set; }

    [Required] public string Category { get; set; } = null!;

    public DateTime Date { get; set; } = DateTime.Now;
}