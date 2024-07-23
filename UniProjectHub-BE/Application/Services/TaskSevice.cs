using Application.InterfaceServies;
using Application.ViewModels;
using Application.ViewModels.ProjectViewModel;
using Application.ViewModels.TaskViewModel;
using Domain.Models;
using Infracstructures;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Users> _userManager;

        public TaskSevice(IUnitOfWork unitOfWork, UserManager<Users> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
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
                Status = taskViewModel.Status,
                Category = taskViewModel.Category,
                Tags = taskViewModel.Tags,
                Deadline = taskViewModel.Deadline,
                Rate = taskViewModel.Rate,
                ProjectId = projectId,
                CreatedAt = TimeHelper.GetVietnamTime(),
                StartDate = taskViewModel.StartDate
            };

            _unitOfWork.TaskRepository.AddEntry(task);
            await _unitOfWork.SaveChangesAsync();

            return taskViewModel;
        }

        public async Task<ShowTask> GetTaskAsync(int id)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return null;
            }

            var showTask = new ShowTask
            {
                Id = task.Id,
                TaskName = task.TaskName,
                Status = task.Status,
                Category = task.Category,
                Tags = task.Tags,
                Deadline = task.Deadline,
                Rate = task.Rate,
                CreatedAt = task.CreatedAt,
                StartDate = task.StartDate
            };
            var subTasks = await _unitOfWork.SubTaskRepository.GetAllSubTasksByTaskIdAsync(id);
            ShowSubTask showSubTask = new ShowSubTask();
            if (subTasks.Any())
            {
                showTask.SubTasks = new List<ShowSubTask>();
                foreach (var subTask in subTasks)
                {
                    showSubTask = new ShowSubTask
                    {
                        Id = subTask.Id,
                        Description = subTask.Description,
                        Deadline = subTask.Deadline,
                        Tags = subTask.Tags
                    };
                    showTask.SubTasks.Add(showSubTask);
                }
            }
            var members = await _unitOfWork.MemberInTaskRepository.GetByTaskIdAsync(id);
            var user = new Users();
            ShowMember showMember = new ShowMember();
            if (members.Any())
            {
                showTask.members = new List<ShowMember>();
                foreach (var member in members)
                {
                    user = _userManager.Users.FirstOrDefault(x => x.Id == member.MemberId);
                    showMember = new ShowMember
                    {
                        Id = member.MemberId,
                        Name = user.UserName
                    };
                    showTask.members.Add(showMember);
                }
            }

            return showTask;
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
                Status = task.Status,
                Category = task.Category,
                Tags = task.Tags,
                Deadline = task.Deadline,
                Rate = task.Rate,
                CreatedAt = task.CreatedAt,
                StartDate = task.StartDate

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
                Status = task.Status,
                Category = task.Category,
                Tags = task.Tags,
                Deadline = task.Deadline,
                Rate = task.Rate,
                StartDate= task.StartDate,
                CreatedAt= task.CreatedAt
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
            task.Status = taskViewModel.Status;
            task.Category = taskViewModel.Category;
            task.Tags = taskViewModel.Tags;
            task.Deadline = taskViewModel.Deadline;
            task.Rate = taskViewModel.Rate;
            task.StartDate = taskViewModel.StartDate;

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
