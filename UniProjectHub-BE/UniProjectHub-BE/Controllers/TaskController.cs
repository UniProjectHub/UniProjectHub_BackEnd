using Application.InterfaceServies;
using Application.Services;
using Application.ViewModels.TaskViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Application.ViewModels.ProjectViewModel.ProjectViewModel;

namespace UniProjectHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("CreateTask/{projectId}")]
        public async Task<IActionResult> CreateTask(int projectId, TaskViewModel request)
        {
            var taskViewModel = await _taskService.CreateTaskAsync(projectId, request);
            if (taskViewModel != null)
                return Ok(); // Task deleted successfully
            else
                return Ok(null); // Task not found or delete operation failed

        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var taskViewModels = await _taskService.GetTasksAsync();
            if (taskViewModels == null || !taskViewModels.Any())
                return Ok(null);

            return Ok(taskViewModels);
        }

        [HttpGet("GetTasksForProjectAsync/{projectId}")]
        public async Task<IActionResult> GetTasksForProjectAsync(int projectId)
        {
            var taskViewModels = await _taskService.GetTasksByProjectIdAsync(projectId);
            if (taskViewModels == null)
                return Ok(null);

            return Ok(taskViewModels);
        }

        [HttpGet("GetTaskAsync/{id}")]
        public async Task<IActionResult> GetTaskAsync(int id)
        {
            var taskViewModels = await _taskService.GetTaskAsync(id);
            if (taskViewModels == null)
                return Ok(null);

            return Ok(taskViewModels);
        }

        [HttpPut("UpdateTask/{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskViewModel request)
        {
            var updatedTaskViewModel = await _taskService.UpdateTaskAsync(id, request);
            if (updatedTaskViewModel == null)
                return Ok(null);

            return Ok(updatedTaskViewModel);
        }

        [HttpDelete("DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (result)
                return Ok(); // Task deleted successfully
            else
                return NotFound(); // Task not found or delete operation failed
        }
    }
}
