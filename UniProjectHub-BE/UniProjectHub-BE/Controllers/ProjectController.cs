using Application.InterfaceServies;
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
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectRequest request)
        {
            var projectViewModel = await _projectService.CreateProjectAsync(request);
            return Ok(projectViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projectViewModels = await _projectService.GetAllProjectsAsync();
            return Ok(projectViewModels);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, UpdateProjectRequest request)
        {
            var updatedProjectViewModel = await _projectService.UpdateProjectAsync(id, request);
            if (updatedProjectViewModel == null)
                return NotFound();

            return Ok(updatedProjectViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (result)
                return Ok(); // Project deleted successfully
            else
                return NotFound(); // Project not found or delete operation failed
        }
    }
}
