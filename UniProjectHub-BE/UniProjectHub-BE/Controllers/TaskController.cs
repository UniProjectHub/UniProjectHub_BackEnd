using Application.InterfaceServies;
using Application.ViewModels.TaskViewModel;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskViewModel request)
        {
            var taskViewModel = await _taskService.CreateTaskAsync(request);
            return Ok(taskViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var taskViewModels = await _taskService.GetTasksAsync();
            return Ok(taskViewModels);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskViewModel request)
        {
            var updatedTaskViewModel = await _taskService.UpdateTaskAsync(id, request);
            if (updatedTaskViewModel == null)
                return NotFound();

            return Ok(updatedTaskViewModel);
        }

        [HttpDelete("{id}")]
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
