using Application.ViewModels.TaskViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface ITaskService
    {
        Task<TaskViewModel> CreateTaskAsync(int procheckId, TaskViewModel taskViewModel);
        Task<ShowTask> GetTaskAsync(int id);
        Task<IEnumerable<TaskViewModel>> GetTasksAsync();
        Task<IEnumerable<TaskViewModel>> GetTasksByProjectIdAsync(int projectId);
        Task<TaskViewModel> UpdateTaskAsync(int id, TaskViewModel taskViewModel);
        Task<bool> DeleteTaskAsync(int id);
    }
}
