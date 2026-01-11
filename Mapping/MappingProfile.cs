using AutoMapper;
using ExpensesTracker.DTOs.Auth;
using ExpensesTracker.DTOs.Budget;
using ExpensesTracker.DTOs.Category;
using ExpensesTracker.DTOs.Expense;
using ExpensesTracker.Models;

namespace ExpensesTracker.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // RegisterDto -> ApplicationUser
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.Email));

            // Expense -> ExpenseDto (exemple futur)
            CreateMap<CreateExpenseDto, Expense>();
            CreateMap<Expense, ReadExpenseDto>();
            // Category
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, ReadCategoryDto>();
            //Budget
            CreateMap<Budget,ReadCategoryDto>();
            CreateMap<CreateBudgetDto,Budget>();

        }
    }
}
