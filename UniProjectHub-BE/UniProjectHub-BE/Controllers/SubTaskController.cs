using Application.InterfaceServies;
using Application.ViewModels.SubTaskViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace UniProjectHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTaskController : ControllerBase
    {
        private readonly ISubTaskService _subTaskService;

        public SubTaskController(ISubTaskService subTaskService)
        {
            _subTaskService = subTaskService;
        }

        // GET api/subtasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubTaskViewModel>>> GetAllSubTasks()
        {
            try
            {
                var subTasks = await _subTaskService.GetAllSubTasksAsync();
                return Ok(subTasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving subtasks");
            }
        }

        // GET api/subtasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubTaskViewModel>> GetSubTaskById(int id)
        {
            try
            {
                var subTask = await _subTaskService.GetSubTaskByIdAsync(id);
                return Ok(subTask);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound("Subtask not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving subtask");
            }
        }

        // GET api/subtasks/task/5
        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<SubTaskViewModel>>> GetSubTasksByTaskId(int taskId)
        {
            var subTasks = await _subTaskService.GetAllSubTasksByTaskIdAsync(taskId);
            return Ok(subTasks);
        }

        // POST api/subtasks
        [HttpPost]
        public async Task<ActionResult<SubTaskViewModel>> CreateSubTaskAsync(CreateSubTaskRequest request)
        {
            try
            {
                var subTaskViewModel = await _subTaskService.CreateSubTaskAsync(request);
                return Ok(subTaskViewModel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/subtasks/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SubTaskViewModel>> UpdateSubTaskAsync(int id, [FromBody] UpdateSubTaskRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var subTaskViewModel = await _subTaskService.UpdateSubTaskAsync(id, request);
            return Ok(subTaskViewModel);
        }
        // Delete a subtask by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubTaskAsync(int id)
        {
            try
            {
                await _subTaskService.DeleteSubTaskAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception, or return a bad request or internal server error response
                return StatusCode(500, ex.Message);
            }
        }

    }
}
