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
        private readonly IValidator<CreateScheduleViewModel> _createValidator;
        private readonly IValidator<UpdateScheduleViewModel> _updateValidator;

        public ScheduleService(
            IScheduleRepository scheduleRepository,
            IMapper mapper,
            IValidator<CreateScheduleViewModel> createValidator,
            IValidator<UpdateScheduleViewModel> updateValidator)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<ScheduleViewModel>> GetAllSchedulesAsync()
        {
            var schedules = await _scheduleRepository.GetAllSchedulesAsync();
            return _mapper.Map<IEnumerable<ScheduleViewModel>>(schedules);
        }
        public async Task<IEnumerable<ScheduleViewModel>> CreateRecurringSchedulesAsync(CreateScheduleViewModel createScheduleViewModel)
        {
            // Validate the input
            var validationResult = await ValidateScheduleAsync(createScheduleViewModel);
            if (!validationResult.IsValid)
            {
                var validationMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(validationMessage);
            }

            var schedules = new List<Schedule>();
            var currentDate = DateTime.Parse(createScheduleViewModel.StartTime.ToShortDateString());

            while (currentDate <= createScheduleViewModel.EndTime)
            {
                if (currentDate.DayOfWeek.ToString() == createScheduleViewModel.DateOfWeek)
                {
                    var schedule = _mapper.Map<Schedule>(createScheduleViewModel);
                    schedule.StartTime = currentDate.Date + createScheduleViewModel.SlotStartTime.TimeOfDay;
                    schedule.EndTime = currentDate.Date + createScheduleViewModel.SlotEndTime.TimeOfDay;
                    schedules.Add(schedule);
                }

                currentDate = currentDate.AddDays(1);
            }

            await _scheduleRepository.AddRangeAsync(schedules);

            return _mapper.Map<IEnumerable<ScheduleViewModel>>(schedules);
        }

        public async Task<ScheduleViewModel> CreateScheduleAsync(CreateScheduleViewModel createScheduleViewModel)
        {
            var validationResult = await ValidateScheduleAsync(createScheduleViewModel);
            if (!validationResult.IsValid)
            {
                var validationMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(validationMessage);
            }

            var schedule = _mapper.Map<Schedule>(createScheduleViewModel);
            await _scheduleRepository.AddAsync(schedule);
            return _mapper.Map<ScheduleViewModel>(schedule);
        }

        public async Task<ScheduleViewModel> UpdateScheduleAsync(int id, UpdateScheduleViewModel updateScheduleViewModel)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);
            if (schedule == null)
                throw new KeyNotFoundException("Schedule not found");

            var validationResult = await ValidateScheduleAsync(updateScheduleViewModel);
            if (!validationResult.IsValid)
            {
                var validationMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(validationMessage);
            }

            _mapper.Map(updateScheduleViewModel, schedule);
            _scheduleRepository.Update(schedule);
            return _mapper.Map<ScheduleViewModel>(schedule);
        }

        public async System.Threading.Tasks.Task DeleteScheduleAsync(int id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);
            if (schedule == null)
                throw new KeyNotFoundException("Schedule not found");

            _scheduleRepository.Delete(schedule);
        }

        public async Task<IEnumerable<ScheduleViewModel>> GetSchedulesByUserIdAsync(string userId)
        {
            var schedules = await _scheduleRepository.GetAllSchedulesAsync(); // Assume this returns IQueryable<Schedule>
            var filteredSchedules = schedules.Where(s => s.UserId == userId);
            return _mapper.Map<IEnumerable<ScheduleViewModel>>(filteredSchedules);
        }

        public async Task<ScheduleViewModel> GetScheduleByIdAsync(int id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);
            return _mapper.Map<ScheduleViewModel>(schedule);
        }

        public async Task<ValidationResult> ValidateScheduleAsync(object model)
        {
            switch (model)
            {
                case CreateScheduleViewModel createModel:
                    return await _createValidator.ValidateAsync(createModel);
                case UpdateScheduleViewModel updateModel:
                    return await _updateValidator.ValidateAsync(updateModel);
                default:
                    throw new ArgumentException("Unknown model type", nameof(model));
            }
        }
    }
}
