using Application.InterfaceServies;
using Application.Validators;
using Application.ViewModels.MemberViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPost("add-member")]
        public async Task<IActionResult> CreateMember([FromBody] MemberViewModel memberViewModel)
        {
            var validator = new MemberViewModelValidator();
            var validationResult = await validator.ValidateAsync(memberViewModel);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // Return validation errors
            }

            try
            {
                var result = await _memberService.CreateMemberAsync(memberViewModel);
                return Ok(result); // 200 OK with the created member or appropriate response
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }

        [HttpPut("update-member/{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberViewModel memberViewModel)
        {
            try
            {
                var result = await _memberService.UpdateMemberAsync(memberViewModel, id);
                if (result == null)
                {
                    return NotFound(); // 404 Not Found if member not found
                }
                return Ok(result); // 200 OK with the updated member or appropriate response
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }

        [HttpDelete("delete-member/{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            try
            {
                await _memberService.DeleteMemberAsync(id);
                return NoContent(); // 204 No Content upon successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }

        [HttpGet("get-members-by-project-id/{projectId}")]
        public async Task<IActionResult> GetMembersByProjectId(int projectId)
        {
            try
            {
                var result = await _memberService.GetMembersByProjectIdAsync(projectId);
                if (result == null || !result.Any())
                {
                    return NotFound(); // 404 Not Found if no members found for the project
                }
                return Ok(result); // 200 OK with the list of members or appropriate response
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }
    }
}
