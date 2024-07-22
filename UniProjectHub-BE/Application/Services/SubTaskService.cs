using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Application.ViewModels;
using Application.ViewModels.SubTaskViewModel;
using AutoMapper;
using Domain.Models;
using Infracstructures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SubTaskService : ISubTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubTaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SubTaskViewModel>> GetAllSubTasksAsync()
        {
            var subTasks = await _unitOfWork.SubTaskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubTask>, IEnumerable<SubTaskViewModel>>(subTasks);
        }

        public async Task<SubTaskViewModel> GetSubTaskByIdAsync(int id)
        {
            var subTask = await _unitOfWork.SubTaskRepository.GetByIdAsync(id);
            if (subTask == null)
            {
                throw new HttpRequestException($"SubTask with id {id} not found", null, HttpStatusCode.NotFound);
            }
            return _mapper.Map<SubTask, SubTaskViewModel>(subTask);
        }
        public async Task<IEnumerable<SubTaskViewModel>> GetAllSubTasksByTaskIdAsync(int taskId)
        {
            var subTasks = await _unitOfWork.SubTaskRepository.GetAllSubTasksByTaskIdAsync(taskId);
            return subTasks.Select(subTask => new SubTaskViewModel
            {
                Id = subTask.Id,
                Description = subTask.Description,
                TaskId = subTask.TaskId,
                Created = subTask.Created,
                Deadline = subTask.Deadline
            });
        }
        // Creates a new subtask
        public async Task<SubTaskViewModel> CreateSubTaskAsync(CreateSubTaskRequest request)
        {
            // Check if the task ID exists
            var taskExists = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
            if (taskExists == null)
            {
                throw new ArgumentException($"Task with ID {request.TaskId} does not exist", nameof(request.TaskId));
            }

            // Map the request to a SubTask entity
            var subTask = _mapper.Map<SubTask>(request);

            // Set the Created date to now if not provided
            if (!request.Created.HasValue)
            {
                subTask.Created = TimeHelper.GetVietnamTime();
            }

            // Add the subtask to the repository
            _unitOfWork.SubTaskRepository.AddEntry(subTask);

            // Save the changes to the database
            await _unitOfWork.SaveChangesAsync();

            // Map the subtask to a SubTaskViewModel and return it
            return _mapper.Map<SubTaskViewModel>(subTask);
        }

        public async Task<SubTaskViewModel> UpdateSubTaskAsync(int id, UpdateSubTaskRequest request)
        {
            var subTask = await _unitOfWork.SubTaskRepository.GetByIdAsync(id);
            if (subTask == null)
            {
                throw new HttpRequestException($"SubTask with id {id} not found", null, HttpStatusCode.NotFound);
            }

            _mapper.Map(request, subTask);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SubTaskViewModel>(subTask);
        }
        public async System.Threading.Tasks.Task DeleteSubTaskAsync(int id)
        {
            // Get the subtask to delete from the repository
            var subTask = await _unitOfWork.SubTaskRepository.GetByIdAsync(id);

            if (subTask == null)
            {
                return; // or log a message, depending on your requirements
            }

            // Delete the subtask from the repository
            _unitOfWork.SubTaskRepository.Delete(subTask);

            // Save the changes to the database
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
