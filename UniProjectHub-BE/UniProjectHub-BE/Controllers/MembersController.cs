using Application.InterfaceServies;
using Application.ViewModels.MemberViewModel;
using Microsoft.AspNetCore.Mvc;

namespace UniProjectHub_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("create-member")]
        public async Task<IActionResult> CreateMember([FromBody] MemberViewModel memberViewModel)
        {
            var result = await _memberService.CreateMemberAsync(memberViewModel);
            return Ok(result);
        }

        [HttpPut("update-member/{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberViewModel memberViewModel)
        {
            var result = await _memberService.UpdateMemberAsync(memberViewModel, id);
            return Ok(result);
        }

      /*  [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            await _memberService.DeleteMemberAsync(id);
            return NoContent();
        }*/

        [HttpGet("get-members-by-project-id/{projectId}")]
        public async Task<IActionResult> GetMembersByProjectId(int projectId)
        {
            var result = await _memberService.GetMembersByProjectIdAsync(projectId);
            return Ok(result);
        }
    }
}