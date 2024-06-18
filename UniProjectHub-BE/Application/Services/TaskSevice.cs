using Application.InterfaceServies;
using Application.ViewModels.ProjectViewModel;
using Application.ViewModels.TaskViewModel;
using Domain.Models;
using Infracstructures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.ViewModels.ProjectViewModel.ProjectViewModel;

namespace Application.Services
{
    public class TaskSevice : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TaskSevice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // Create
        public async Task<TaskViewModel> CreateTaskAsync(int projectId, TaskViewModel taskViewModel)
        {
            var projects = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
            if (projects == null)
            {
                return null;
            }
            var task = new Domain.Models.Task
            {
                TaskName = taskViewModel.TaskName,
                Description = taskViewModel.Description,
                Status = taskViewModel.Status,
                Category = taskViewModel.Category,
                Tags = taskViewModel.Tags,
                Deadline = taskViewModel.Deadline,
                Rate = taskViewModel.Rate,
                Comment = taskViewModel.Comment,
                ProjectId = projectId
            };

            _unitOfWork.TaskRepository.AddEntry(task);
            await _unitOfWork.SaveChangesAsync();

            return taskViewModel;
        }

        public async Task<TaskViewModel> GetTaskAsync(int id)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return null;
            }

            var taskViewModel = new TaskViewModel
            {
                Id = task.Id,
                TaskName = task.TaskName,
                Description = task.Description,
                Status = task.Status,
                Category = task.Category,
                Tags = task.Tags,
                Deadline = task.Deadline,
                Rate = task.Rate,
                Comment = task.Comment
            };

            return taskViewModel;
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasksByProjectIdAsync(int projectId)
        {
            var projects = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
            if (projects == null)
            {
                return null;
            }
            var tasks = await _unitOfWork.TaskRepository.GetTasksByProjectIdAsync(projectId);

            var taskViewModels = tasks.Select(task => new TaskViewModel
            {
                Id= task.Id,
                TaskName = task.TaskName,
                Description = task.Description,
                Status = task.Status,
                Category = task.Category,
                Tags = task.Tags,
                Deadline = task.Deadline,
                Rate = task.Rate,
                Comment = task.Comment
            });

            return taskViewModels;
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasksAsync()
        {
            var tasks = await _unitOfWork.TaskRepository.GetAllAsync();

            var taskViewModels = tasks.Select(task => new TaskViewModel
            {
                Id = task.Id,
                TaskName = task.TaskName,
                Description = task.Description,
                Status = task.Status,
                Category = task.Category,
                Tags = task.Tags,
                Deadline = task.Deadline,
                Rate = task.Rate,
                Comment = task.Comment
            });

            return taskViewModels;
        }

        public async Task<TaskViewModel> UpdateTaskAsync(int id, TaskViewModel taskViewModel)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return null;
            }

            task.TaskName = taskViewModel.TaskName;
            task.Description = taskViewModel.Description;
            task.Status = taskViewModel.Status;
            task.Category = taskViewModel.Category;
            task.Tags = taskViewModel.Tags;
            task.Deadline = taskViewModel.Deadline;
            task.Rate = taskViewModel.Rate;
            task.Comment = taskViewModel.Comment;

            await _unitOfWork.SaveChangesAsync();
            return taskViewModel;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return false;
            }

            _unitOfWork.TaskRepository.Delete(task);
            var result = await _unitOfWork.SaveChangesAsync() > 0; // Check if any changes were saved

            return result;
        }
    }
}
