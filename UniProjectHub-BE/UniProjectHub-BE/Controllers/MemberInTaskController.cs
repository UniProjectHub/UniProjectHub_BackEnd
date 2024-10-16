﻿using Application.InterfaceServies;
using Application.ViewModels.MemberInTaskViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UniProjectHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberInTaskController : ControllerBase
    {
        private readonly IMemberInTaskService _memberInTaskService;

        public MemberInTaskController(IMemberInTaskService memberInTaskService)
        {
            _memberInTaskService = memberInTaskService;
        }

        [HttpGet("GetAllAsync")]
        // Retrieves a list of all MemberInTask entities
        public async Task<ActionResult<IEnumerable<MemberInTaskViewModel>>> GetAllAsync()
        {
            var memberInTasks = await _memberInTaskService.GetAllAsync();
            if (memberInTasks == null || !memberInTasks.Any())
            {
                return Ok(null);
            }
            return Ok(memberInTasks);
        }

        [HttpGet("GetByIdAsync/{id}")]
        // Retrieves a single MemberInTask entity by ID
        public async Task<ActionResult<MemberInTaskViewModel>> GetByIdAsync(int id)
        {
            var memberInTask = await _memberInTaskService.GetByIdAsync(id);
            if (memberInTask == null)
            {
                return Ok(null);
            }
            return Ok(memberInTask);
        }

        [HttpGet("GetByTaskIdAsync/{taskId}")]
        // Retrieves a list of MemberInTask entities by Task ID
        public async Task<ActionResult<IEnumerable<MemberInTaskViewModel>>> GetByTaskIdAsync(int taskId)
        {
            var memberInTasks = await _memberInTaskService.GetByTaskIdAsync(taskId);
            if (memberInTasks == null || !memberInTasks.Any())
            {
                return Ok(null);
            }
            return Ok(memberInTasks);
        }
        [HttpPost("Create")]
        // Creates a new MemberInTask entity
        public async Task<ActionResult<MemberInTaskViewModel>> Create(MemberInTaskCreateModel model)
        {
            var result = await _memberInTaskService.CreateAsync(model);
            return Ok(result);
        }
        [HttpPut("Update/{id}")]
        // Updates an existing MemberInTask entity
        public async Task<ActionResult<MemberInTaskViewModel>> Update(int id, MemberInTaskUpdateModel model)
        {
            var result = await _memberInTaskService.UpdateAsync(id, model);
            if (result == null)
            {
                return Ok(null);
            }
            return Ok(result);
        }
        [HttpDelete("Delete/{id}")]
        // Deletes a MemberInTask entity by ID
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var result = await _memberInTaskService.DeleteAsync(id);
                if (!result)
                {
                    return Ok(null);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error deleting member in task");
            }
        }
    }
}
