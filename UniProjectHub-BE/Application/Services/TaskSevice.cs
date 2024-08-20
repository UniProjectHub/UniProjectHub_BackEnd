using Application.InterfaceServies;
using Application.ViewModels;
using Application.ViewModels.TaskViewModel;
using Domain.Models;
using Infracstructures;
using Microsoft.AspNetCore.Identity;

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
        public async Task<CreateTaskModel> CreateTaskAsync(int projectId, CreateTaskModel taskViewModel)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
            if (project == null)
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
                StartDate = taskViewModel.StartDate,
                RemainingTime = taskViewModel.RemainingTime,
                OwnerId = taskViewModel.OwnerId,  
            };

            Console.WriteLine($"Task OwnerId before saving: {task.OwnerId}");

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
                ProjectId = task.ProjectId,
                TaskName = task.TaskName,
                Status = task.Status,
                Category = task.Category,
                Tags = task.Tags,
                Deadline = task.Deadline,
                Rate = task.Rate,
                CreatedAt = task.CreatedAt,
                StartDate = task.StartDate,
                RemainingTime = task.RemainingTime,
                OwnerId = task.OwnerId
            };

            var subTasks = await _unitOfWork.SubTaskRepository.GetAllSubTasksByTaskIdAsync(id);
            if (subTasks.Any())
            {
                showTask.SubTasks = subTasks.Select(subTask => new ShowSubTask
                {
                    Id = subTask.Id,
                    Description = subTask.Description,
                    Deadline = subTask.Deadline,
                    Tags = subTask.Tags
                }).ToList();
            }

            var members = await _unitOfWork.MemberInTaskRepository.GetByTaskIdAsync(id);
            if (members.Any())
            {
                showTask.members = members.Select(member => new ShowMember
                {
                    Id = member.MemberId,
                    Name = _userManager.Users.FirstOrDefault(x => x.Id == member.MemberId)?.UserName
                }).ToList();
            }

            return showTask;
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasksByProjectIdAsync(int projectId)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                return null;
            }

            var tasks = await _unitOfWork.TaskRepository.GetTasksByProjectIdAsync(projectId);

            return tasks.Select(task => new TaskViewModel
            {
                Id = task.Id,
                TaskName = task.TaskName,
                Status = task.Status,
                Category = task.Category,
                Tags = task.Tags,
                Deadline = task.Deadline,
                Rate = task.Rate,
                CreatedAt = task.CreatedAt,
                StartDate = task.StartDate,
                RemainingTime = task.RemainingTime,
                OwnerId = task.OwnerId
            });
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasksAsync()
        {
            var tasks = await _unitOfWork.TaskRepository.GetAllAsync();

            return tasks.Select(task => new TaskViewModel
            {OwnerId = task.OwnerId,
                Id = task.Id,
                TaskName = task.TaskName,
                Status = task.Status,
                Category = task.Category,
                Tags = task.Tags,
                Deadline = task.Deadline,
                Rate = task.Rate,
                CreatedAt = task.CreatedAt,
                StartDate = task.StartDate,
                RemainingTime = task.RemainingTime
            });
        }

        public async Task<UpdateTaskModel> UpdateTaskAsync(int id, UpdateTaskModel taskViewModel)
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
            task.RemainingTime = taskViewModel.RemainingTime;

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
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}