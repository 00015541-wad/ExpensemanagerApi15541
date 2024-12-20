﻿using ExpenseManagerApi.Models;

namespace ExpenseManagerApi.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
