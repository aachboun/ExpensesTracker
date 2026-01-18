using ExpensesTracker.DTOs;
using ExpensesTracker.DTOs.Budget;
using FluentValidation;

namespace ExpensesTracker.Validators
{
    public class CreateBudgetDtoValidator : AbstractValidator<CreateBudgetDto>
    {
        public CreateBudgetDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0);

          
        }
    }
}
