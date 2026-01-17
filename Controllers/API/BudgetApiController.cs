using ExpensesTracker.DTOs.Budget;
using ExpensesTracker.Models;
using ExpensesTracker.Services;
using ExpensesTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ExpensesTracker.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetApiController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BudgetApiController(IBudgetService budgetService, UserManager<ApplicationUser> userManager)
        {
            _budgetService = budgetService;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBudgetAsync([FromBody] CreateBudgetDto dto)
        {
            return null;

        }
    }
}
