using Application.InterfaceServies;
using Application.ViewModels.ProjectViewModel;
using Domain.Models;
using Infracstructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.ViewModels.ProjectViewModel.ProjectViewModel;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // Create
        public async Task<ProjectViewModel> CreateProjectAsync(CreateProjectRequest request)
        {
            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                IsGroup = request.IsGroup,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.ProjectRepository.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            return new ProjectViewModel
            {
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
                IsGroup = project.IsGroup,
                CreatedAt = project.CreatedAt
            };
        }

        // Read
        public async Task<IEnumerable<ProjectViewModel>> GetAllProjectsAsync()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAllAsync();
            return projects.Select(p => new ProjectViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Status = p.Status,
                IsGroup = p.IsGroup,
                CreatedAt = p.CreatedAt
            });
        }

        public async Task<ProjectViewModel> GetProjectByIdAsync(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                return null;

            return new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
                IsGroup = project.IsGroup,
                CreatedAt = project.CreatedAt
            };
        }

        // Update
        public async Task<ProjectViewModel> UpdateProjectAsync(int id, UpdateProjectRequest request)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                return null;

            project.Name = request.Name;
            project.Description = request.Description;
            project.Status = request.Status;
            project.IsGroup = request.IsGroup;

            _unitOfWork.ProjectRepository.Update(project);
            await _unitOfWork.SaveChangesAsync();

            return new ProjectViewModel
            {
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
                IsGroup = project.IsGroup,
                CreatedAt = project.CreatedAt
            };
        }

        // Delete
        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                return false; // Return false if the project is not found

            _unitOfWork.ProjectRepository.Delete(project);
            var result = await _unitOfWork.SaveChangesAsync() > 0; // Check if any changes were saved

            return result;
        }
    }

}

