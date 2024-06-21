using Application.InterfaceServies;
using Application.ViewModels.BlogModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UniProjectHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost("CreateBlogAsync")]
        public async Task<ActionResult<BlogModelView>> CreateBlogAsync(BlogCreateModel blogCreateModel)
        {
            var blog = await _blogService.CreateBlogAsync(blogCreateModel);
            return CreatedAtAction(nameof(GetBlogAsync), new { id = blog.Id }, blog);
        }

        [HttpGet("GetBlogAsync/{id}")]
        public async Task<ActionResult<BlogModelView>> GetBlogAsync(int id)
        {
            var blog = await _blogService.GetBlogAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpGet("GetBlogsByCategoryIdAsync/{categoryId}")]
        public async Task<ActionResult<IEnumerable<BlogModelView>>> GetBlogsByCategoryIdAsync(int categoryId)
        {
            var blogs = await _blogService.GetBlogsByCategoryIdAsync(categoryId);
            return Ok(blogs);
        }

        [HttpGet("GetBlogsAsync")]
        public async Task<ActionResult<IEnumerable<BlogModelView>>> GetBlogsAsync()
        {
            var blogs = await _blogService.GetBlogsAsync();
            return Ok(blogs);
        }

        [HttpPut("UpdateBlogAsync/{id}")]
        public async Task<ActionResult> UpdateBlogAsync(int id, BlogUpdateModel blogUpdateModel)
        {
            await _blogService.UpdateBlogAsync(blogUpdateModel);
            return NoContent();
        }

        [HttpDelete("DeleteBlogAsync/{id}")]
        public async Task<ActionResult> DeleteBlogAsync(int id)
        {
            await _blogService.DeleteBlogAsync(id);
            return NoContent();
        }
    }
}
