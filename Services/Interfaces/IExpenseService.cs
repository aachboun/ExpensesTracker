using ExpensesTracker.DTOs.Expense;





namespace ExpensesTracker.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<int> CreateAsync(CreateExpenseDto dto , string UserId);
        Task DeleteAsync( int CategoryId ,string UserId);
        Task<ReadExpenseDto> GetExpense(int CategoryId ,string UserId);
        Task<List<ReadExpenseDto>> GetAllExpenses(string  UserId);
        Task<List<ReadExpenseDto>> GetByPeriod( string UserId, DateOnly From, DateOnly To);
        Task<List<ReadExpenseDto>> GetByCatAndPeriod(int CategoryId, string UserId, DateOnly From, DateOnly To);
        Task UpdateExpense(int CategoryId ,UpdateExpenseDto dto, string UserId);
    }
}
