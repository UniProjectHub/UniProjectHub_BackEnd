using Application.InterfaceServies;
using Application.ViewModels.GroupChatViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace UniProjectHub_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupChatsController : ControllerBase
    {
        private readonly IGroupChatService _groupChatService;

        public GroupChatsController(IGroupChatService groupChatService)
        {
            _groupChatService = groupChatService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllGroupChats()
        {
            var groupChats = await _groupChatService.GetAllGroupChatsAsync();
            var result = groupChats.Select(gc => new
            {
                gc.ProjectId,
                gc.MemberId,
                gc.Messenger,
                gc.Status
            });
            return Ok(result);
        }
        [HttpPost("create-group-chat")]
        public async Task<IActionResult> CreateGroupChat([FromBody] GroupChatViewModel groupChatViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _groupChatService.AddGroupChatAsync(groupChatViewModel);
            return Ok();
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetGroupChatById(int id)
        {
            var groupChat = await _groupChatService.GetGroupChatByIdAsync(id);
            if (groupChat == null)
            {
                return NotFound();
            }
            return Ok(groupChat);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateGroupChat(int id, [FromBody] GroupChatViewModel groupChatViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _groupChatService.UpdateGroupChatAsync(groupChatViewModel, id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("GroupChat not found");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupChat(int id)
        {
            try
            {
                await _groupChatService.DeleteGroupChatAsync(id);
                return Ok(); // Or appropriate status code
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
