using Microsoft.AspNetCore.Identity;

namespace ExpensesTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Category> Categories { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}
