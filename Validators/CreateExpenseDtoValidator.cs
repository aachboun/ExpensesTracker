using ExpensesTracker.DTOs;
using ExpensesTracker.DTOs.Expense;
using FluentValidation;

namespace ExpensesTracker.Validators
{
    public class CreateExpenseDtoValidator : AbstractValidator<CreateExpenseDto>
    {
        public CreateExpenseDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0");

            RuleFor(x => x.Date)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Expense date cannot be in the future");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("Category is required");

            RuleFor(x => x.Description)
                .MaximumLength(300);
        }
    }
}

