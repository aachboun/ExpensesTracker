using ExpensesTracker.DTOs.Expense;





namespace ExpensesTracker.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<int> CreateAsync(CreateExpenseDto dto , string UserId,int CategoryId);
        Task DeleteAsync( int Id ,string UserId);
        Task<ReadExpenseDto> GetExpense(int Id ,string UserId);
        Task<List<ReadExpenseDto>> GetAllExpenses(string  UserId);
        Task<List<ReadExpenseDto>> GetByPeriod( string UserId, DateOnly From, DateOnly To);
        Task<List<ReadExpenseDto>> GetByCatAndPeriod(int CategoryId, string UserId, DateOnly From, DateOnly To);
        Task UpdateExpense(int Id ,UpdateExpenseDto dto, string UserId);
    }
}
