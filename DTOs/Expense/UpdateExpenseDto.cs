namespace ExpensesTracker.DTOs.Expense
{
    public class UpdateExpenseDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }

    
    }
}
