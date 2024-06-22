using Application.InterfaceServies;
using Application.ViewModels.CommentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace UniProjectHub_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController :ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("CreateCommentAsync")]
        public async Task<ActionResult<CommentViewModel>> CreateCommentAsync([FromBody] CommentViewModel commentViewModel)
        {
            try
            {
                var result = await _commentService.CreateCommentAsync(commentViewModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error creating comment");
            }
        }

        [HttpGet("GetCommentAsync/{id}")]
        public async Task<ActionResult<CommentViewModel>> GetCommentAsync(int id)
        {
            try
            {
                var result = await _commentService.GetCommentAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error getting comment");
            }
        }

        [HttpGet("GetCommentsAsync")]
        public async Task<ActionResult<IEnumerable<CommentViewModel>>> GetCommentsAsync()
        {
            try
            {
                var result = await _commentService.GetCommentsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error getting comments");
            }
        }
        [HttpPut("UpdateCommentAsync/{id}")]
        public async Task<ActionResult> UpdateCommentAsync(int id, [FromBody] CommentViewModel commentViewModel)
        {
            if (id != commentViewModel.Id)
            {
                return BadRequest("Id in the route does not match the Id in the request body");
            }
            try
            {
                await _commentService.UpdateCommentAsync(commentViewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error updating comment");
            }
        }

        [HttpDelete("DeleteCommentAsync/{id}")]
        public async Task<ActionResult> DeleteCommentAsync(int id)
        {
            try
            {
                await _commentService.DeleteCommentAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error deleting comment");
            }
        }

        [HttpGet("GetAllCommentsByBlogIdAsync/{blogId}")]
        public async Task<ActionResult<IEnumerable<CommentViewModel>>> GetAllCommentsByBlogIdAsync(int blogId)
        {
            try
            {
                var result = await _commentService.GetAllCommentsByBlogIdAsync(blogId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error getting comments by blog ID");
            }
        }
    }
}
