using Application.InterfaceServies;
using Application.ViewModels.GroupChatViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> CreateGroupChat([FromBody] GroupChatViewModel groupChatViewModel)
        {
            var result = await _groupChatService.CreateGroupChatAsync(groupChatViewModel);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroupChat(int id, [FromBody] GroupChatViewModel groupChatViewModel)
        {
            var result = await _groupChatService.UpdateGroupChatAsync(groupChatViewModel, id);
            return Ok(result);
        }

   /*     [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupChat(int id)
        {
            await _groupChatService.DeleteGroupChatAsync(id);
            return NoContent();
        }*/

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetGroupChatsByProjectId(int projectId)
        {
            var result = await _groupChatService.GetGroupChatsByProjectIdAsync(projectId);
            return Ok(result);
        }
    }
}
