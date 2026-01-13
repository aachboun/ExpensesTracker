namespace ExpensesTracker.Models
{
    public class Budget
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateOnly EffectiveFrom { get; set; }
        public DateOnly? EffectiveTo { get; set; }
       

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
