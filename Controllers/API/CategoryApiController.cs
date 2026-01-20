
using ExpensesTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ExpensesTracker.Models;
using ExpensesTracker.DTOs.Category;


namespace ExpensesTracker.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CategoryApiController(ICategoryService categoryService, UserManager<ApplicationUser> userManager)
        {
           _categoryService = categoryService;
           _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryDto Dto)
        {
            var UserId = _userManager.GetUserId(User);
            var CategoryId = await _categoryService.CreateAsync(Dto, UserId);

            return CreatedAtAction(
                nameof(GetByIdAsync), new { CategoryId }, null);

        }
        [HttpGet("{CategoryId:int}")]
        public async Task<IActionResult> GetByIdAsync(int CategoryId)
        {
            var UserId = _userManager.GetUserId(User);
            var category = await _categoryService.GetByIdAsync(CategoryId, UserId);
            return Ok(category);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var UserId = _userManager.GetUserId(User);
            var categories = await _categoryService.GetAllAsync(UserId);

            return Ok(categories);
        }
        [HttpDelete("{CategoryId:int}")]
        public async Task<IActionResult> DeleteAsync(int CategoryId)
        {
            var UserId = _userManager.GetUserId(User);
            await _categoryService.DeleteAsync(CategoryId,UserId);
            return NoContent();

        }
        [HttpPut("{CategoryId:int}")]
        public async Task<IActionResult> UpdateCategory(int CategoryId, [FromBody] UpdateCategoryDto dto)
        {
            var UserId = _userManager.GetUserId(User);
            await _categoryService.UpdateAsync(CategoryId,dto, UserId);
            return NoContent();
        }

    }

}
