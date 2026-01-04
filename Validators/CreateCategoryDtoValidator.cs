using ExpensesTracker.DTOs;
using ExpensesTracker.DTOs.Category;
using FluentValidation;

namespace ExpensesTracker.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
