using Application.InterfaceServies;
using Application.ViewModels.CategoryViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UniProjectHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("CreateCategoryAsync")]
        public async Task<ActionResult<CategoryViewModel>> CreateCategoryAsync([FromBody] CategoryViewModel categoryViewModel)
        {
            try
            {
                var result = await _categoryService.CreateCategoryAsync(categoryViewModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error creating category");
            }
        }

        [HttpGet("GetCategoryAsync/{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetCategoryAsync(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return Ok(null);
            }
            return category;
        }

        [HttpGet("GetCategoriesAsync")]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpPut("UpdateCategoryAsync/{id}")]
        public async Task<ActionResult> UpdateCategoryAsync(int id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                return BadRequest();
            }
            await _categoryService.UpdateCategoryAsync(categoryViewModel);
            return NoContent();
        }

        [HttpDelete("DeleteCategoryAsync/{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}

