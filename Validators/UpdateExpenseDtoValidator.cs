using ExpensesTracker.DTOs;
using ExpensesTracker.DTOs.Expense;
using FluentValidation;

namespace ExpensesTracker.Validators
{
    public class UpdateExpenseDtoValidator : AbstractValidator<UpdateExpenseDto>
    {
        public UpdateExpenseDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0);

            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.UtcNow);

            RuleFor(x => x.Description)
                .MaximumLength(300);
        }
    }
}

