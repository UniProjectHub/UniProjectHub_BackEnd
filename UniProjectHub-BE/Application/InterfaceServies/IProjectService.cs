using Application.ViewModels.ProjectViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.ViewModels.ProjectViewModel.ProjectViewModel;

namespace Application.InterfaceServies
{
    public interface IProjectService
    {
        Task<ProjectViewModel> CreateProjectAsync(CreateProjectRequest request, string ownerId);
        Task<IEnumerable<ProjectViewModel>> GetAllProjectsAsync();
        Task<ProjectViewModel> GetProjectByIdAsync(int id);
        Task<IEnumerable<ProjectViewModel>> GetGroupProjectsByUserAsync(string userId);
        Task<ProjectViewModel> UpdateProjectAsync(int id, UpdateProjectRequest request);
        Task<bool> DeleteProjectAsync(int id);
        Task<IEnumerable<ProjectViewModel>> GetProjectsByUserOwnerAsync(string userId);
        Task<IEnumerable<ProjectViewModel>> GetProjectsByUserAsync(string userId);
    }
}
