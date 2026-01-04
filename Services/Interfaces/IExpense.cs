using ExpensesTracker.DTOs.Expense;





namespace ExpensesTracker.Services.Interfaces
{
    public interface IExpense
    {
        Task CreateAsync(CreateExpenseDto dto , string UserId);
        Task DeleteAsync(string UserId);
        Task GetExpense(int id ,string UserId);
        Task GetAllExpenses(string  UserId);   
        Task UpdateExpense(int id , string UserId);
    }
}
