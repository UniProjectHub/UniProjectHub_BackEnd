using Application.InterfaceServies;
using Application.Services;
using Application.ViewModels.ProjectViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.ViewModels.ProjectViewModel.ProjectViewModel;

namespace UniProjectHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpPost("CreateProject/{ownerId}")]
        public async Task<IActionResult> CreateProject(CreateProjectRequest request, string ownerId)
        {
            var projectViewModel = await _projectService.CreateProjectAsync(request, ownerId);
            return Ok(projectViewModel);
        }

        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projectViewModels = await _projectService.GetAllProjectsAsync();
            if (projectViewModels == null || !projectViewModels.Any())
                return Ok(null);

            return Ok(projectViewModels);
        }

        [HttpGet("GetProjectById/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var projectViewModels = await _projectService.GetProjectByIdAsync(id);
            if (projectViewModels == null)
                return Ok(null);

            return Ok(projectViewModels);
        }

        [HttpGet("GetProjectsByUserOwner/{userId}")]
        public async Task<ActionResult<IEnumerable<ProjectViewModel>>> GetProjectsByUserOwner(string userId)
        {
            try
            {
                var projects = await _projectService.GetProjectsByUserOwnerAsync(userId);
                if (projects == null || !projects.Any())
                {
                    return Ok(null); // or return Ok(Enumerable.Empty<ProjectViewModel>());
                }
                return Ok(projects);
            }
            catch (Exception ex)
            {
                // log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("GetProjectsByUserAsync/{userId}")]
        public async Task<ActionResult<IEnumerable<ProjectViewModel>>> GetProjectsByUserAsync(string userId)
        {
            try
            {
                var projects = await _projectService.GetProjectsByUserAsync(userId);
                if (projects == null || !projects.Any())
                {
                    return Ok(null); // 404 Not Found
                }
                return Ok(projects);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while retrieving projects"); // 500 Internal Server Error
            }
        }
        [HttpGet("GetGroupProjectsByUserAsync/{userId}")]
        public async Task<ActionResult<IEnumerable<ProjectViewModel>>> GetGroupProjectsByUserAsync(string userId)
        {
            try
            {
                var projects = await _projectService.GetGroupProjectsByUserAsync(userId);
                if (projects == null || !projects.Any())
                {
                    return Ok(null); // 404 Not Found
                }
                return Ok(projects);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while retrieving projects"); // 500 Internal Server Error
            }
        }

        [HttpPut("UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(int id, UpdateProjectRequest request)
        {
            var updatedProjectViewModel = await _projectService.UpdateProjectAsync(id, request);
            if (updatedProjectViewModel == null)
                return Ok(null);

            return Ok(updatedProjectViewModel);
        }

        [HttpDelete("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (result)
                return Ok(); // Project deleted successfully
            else
                return Ok(null); // Project not found or delete operation failed
        }
    }
}
