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
                gc.Id,
                gc.ProjectId,
                gc.MemberId,
                gc.Messenger,
                gc.Status
            });
            return Ok(result);
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
            var result = await _groupChatService.UpdateGroupChatAsync(groupChatViewModel, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGroupChat(int id)
        {
            await _groupChatService.DeleteGroupChatAsync(id);
            return NoContent();
        }
    }
}
