using Application.InterfaceServies;
using Application.ViewModels.MemberInTaskViewModel;
using Application.ViewModels.SubTaskViewModel;
using AutoMapper;
using Domain.Models;
using Infracstructures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MemberInTaskService : IMemberInTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberInTaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MemberInTaskViewModel>> GetAllAsync()
        {
            var memberInTasks = await _unitOfWork.MemberInTaskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberInTask>, IEnumerable<MemberInTaskViewModel>>(memberInTasks);
        }

        public async Task<MemberInTaskViewModel> GetByIdAsync(int id)
        {
            var memberInTask = await _unitOfWork.MemberInTaskRepository.GetByIdAsync(id);
            return _mapper.Map<MemberInTask, MemberInTaskViewModel>(memberInTask);
        }

        public async Task<IEnumerable<MemberInTaskViewModel>> GetByTaskIdAsync(int taskId)
        {
            var memberInTasks = await _unitOfWork.MemberInTaskRepository.GetByTaskIdAsync(taskId);
            return _mapper.Map<IEnumerable<MemberInTask>, IEnumerable<MemberInTaskViewModel>>(memberInTasks);
        }
        public async Task<MemberInTaskViewModel> CreateAsync(MemberInTaskCreateModel model)
        {
            var taskExists = await _unitOfWork.TaskRepository.GetByIdAsync(model.TaskId);
            if (taskExists == null)
            {
                throw new ArgumentException("Invalid TaskId", nameof(model.TaskId));
            }

            var subTask = _mapper.Map<MemberInTaskCreateModel, SubTask>(model);
            await _unitOfWork.SubTaskRepository.AddAsync(subTask);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SubTask, MemberInTaskViewModel>(subTask);
        }

        public async Task<MemberInTaskViewModel> UpdateAsync(int id, MemberInTaskUpdateModel model)
        {
            var memberInTask = await _unitOfWork.MemberInTaskRepository.GetByIdAsync(id);
            if (memberInTask == null)
            {
                return null; // or throw a custom exception if you prefer
            }
            _mapper.Map(model, memberInTask);
            _unitOfWork.MemberInTaskRepository.Update(memberInTask);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<MemberInTask, MemberInTaskViewModel>(memberInTask);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.MemberInTaskRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _unitOfWork.MemberInTaskRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
