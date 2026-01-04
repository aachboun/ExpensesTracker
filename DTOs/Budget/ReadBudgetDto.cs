namespace ExpensesTracker.DTOs.Budget
{
    public class ReadBudgetDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
