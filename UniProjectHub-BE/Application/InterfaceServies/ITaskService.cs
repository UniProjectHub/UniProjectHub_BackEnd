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
        Task<ShowTask> GetTaskAsync(int id);
        Task<CreateTaskModel> CreateTaskAsync(int projectId, CreateTaskModel taskViewModel);
       
        Task<IEnumerable<TaskViewModel>> GetTasksAsync();
        Task<IEnumerable<TaskViewModel>> GetTasksByProjectIdAsync(int projectId);
        Task<UpdateTaskModel> UpdateTaskAsync(int id, UpdateTaskModel taskViewModel); // Ensure this matches
        Task<bool> DeleteTaskAsync(int id);
    }
}