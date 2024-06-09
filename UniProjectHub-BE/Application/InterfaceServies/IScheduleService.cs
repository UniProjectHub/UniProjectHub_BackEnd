using Application.ViewModels.ScheduleViewModel;
using Domain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface IScheduleService
    {
        Task<ValidationResult> ValidateScheduleAsync(ScheduleViewModel scheduleViewModel);
        Task<Schedule> CreateScheduleAsync(ScheduleViewModel scheduleViewModel);
        Task<Schedule> UpdateScheduleAsync(ScheduleViewModel scheduleViewModel, int id);
        Task<IEnumerable<ScheduleViewModel>> GetSchedulesByUserIdAsync(string userId);
        Task<ScheduleViewModel> GetScheduleByIdAsync(int id);
        System.Threading.Tasks.Task DeleteScheduleAsync(int id);
    }
}
