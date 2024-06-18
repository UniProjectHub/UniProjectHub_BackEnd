using Application.ViewModels.SubTaskViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface ISubTaskService
    {
        Task<IEnumerable<SubTaskViewModel>> GetAllSubTasksAsync();
        Task<SubTaskViewModel> GetSubTaskByIdAsync(int id);
        Task<SubTaskViewModel> CreateSubTaskAsync(CreateSubTaskRequest request);
        Task<SubTaskViewModel> UpdateSubTaskAsync(int id, UpdateSubTaskRequest request);
        Task<IEnumerable<SubTaskViewModel>> GetAllSubTasksByTaskIdAsync(int taskId);
        System.Threading.Tasks.Task DeleteSubTaskAsync(int id);
        }
}
