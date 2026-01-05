using ExpensesTracker.DTOs.Expense;





namespace ExpensesTracker.Services.Interfaces
{
    public interface IExpense
    {
        Task CreateAsync(CreateExpenseDto dto , string UserId);
        Task DeleteAsync( int id ,string UserId);
        Task<ReadExpenseDto> GetExpense(int id ,string UserId);
        Task<List<ReadExpenseDto>> GetAllExpenses(string  UserId);   
        Task UpdateExpense(int id ,UpdateExpenseDto dto, string UserId);
    }
}
