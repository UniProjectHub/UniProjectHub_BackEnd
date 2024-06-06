using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Application.ViewModels.ScheduleViewModel;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ScheduleViewModel> _validator;

        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper, IValidator<ScheduleViewModel> validator)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ValidationResult> ValidateScheduleAsync(ScheduleViewModel scheduleViewModel)
        {
            return await _validator.ValidateAsync(scheduleViewModel);
        }

        public async Task<Schedule> CreateScheduleAsync(ScheduleViewModel scheduleViewModel)
        {
            var validationResult = await ValidateScheduleAsync(scheduleViewModel);
            if (!validationResult.IsValid)
            {
                var validationResults = validationResult.Errors
                    .Select(e => new System.ComponentModel.DataAnnotations.ValidationResult(e.ErrorMessage, new[] { e.PropertyName }))
                    .ToList();
                var validationMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException(validationMessage, (IEnumerable<ValidationFailure>)validationResults);
            }

            var schedule = _mapper.Map<Schedule>(scheduleViewModel);
            await _scheduleRepository.AddAsync(schedule);
            return schedule;
        }

        public async Task<Schedule> UpdateScheduleAsync(ScheduleViewModel scheduleViewModel, int id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);
            if (schedule == null)
                throw new KeyNotFoundException("Schedule not found");

            var validationResult = await ValidateScheduleAsync(scheduleViewModel);
            if (!validationResult.IsValid)
            {
                var validationResults = validationResult.Errors
                    .Select(e => new System.ComponentModel.DataAnnotations.ValidationResult(e.ErrorMessage, new[] { e.PropertyName }))
                    .ToList();
                var validationMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException(validationMessage, (IEnumerable<ValidationFailure>)validationResults);
            }

            _mapper.Map(scheduleViewModel, schedule);
            _scheduleRepository.Update(schedule);
            return schedule;
        }

        public async Task<IEnumerable<ScheduleViewModel>> GetSchedulesByUserIdAsync(string userId)
        {
            var schedules = await _scheduleRepository.GetAllAsync(s => (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Schedule, object>)s.Where(sc => sc.UserId == userId));
            return _mapper.Map<IEnumerable<ScheduleViewModel>>(schedules);
        }

        public async Task<ScheduleViewModel> GetScheduleByIdAsync(int id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);
            return _mapper.Map<ScheduleViewModel>(schedule);
        }

       public async System.Threading.Tasks.Task DeleteScheduleAsync(int id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);
            if (schedule == null)
                throw new KeyNotFoundException("Schedule not found");

            _scheduleRepository.Delete(schedule);
        }

        System.Threading.Tasks.Task IScheduleService.DeleteScheduleAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
