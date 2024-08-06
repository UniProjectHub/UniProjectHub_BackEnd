using Application.InterfaceServies;
using Application.Validators;
using Application.ViewModels.MemberViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberViewModel createMemberView)
        {
            var validator = new CreateMemberViewModelValidator();
            var validationResult = await validator.ValidateAsync(createMemberView);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var result = await _memberService.CreateMemberAsync(createMemberView);
                return Ok(new
                {
                    Id = result.Id,
                    ProjectId = result.ProjectId,
                    MenberId = result.MenberId,
                    IsOwner = result.IsOwner,
                    Role = result.Role,
                    JoinTime = result.JoinTime,
                }); // Return the member details
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
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
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message); // 400 Bad Request with validation error message
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
                return Ok("Xóa thành công Members");
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

        [HttpGet("get-all-members")]
        public async Task<IActionResult> GetAllMembers()
        {
            try
            {
                var result = await _memberService.GetAllMembersAsync();
                if (result == null || !result.Any())
                {
                    return NotFound(); // 404 Not Found if no members found
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
