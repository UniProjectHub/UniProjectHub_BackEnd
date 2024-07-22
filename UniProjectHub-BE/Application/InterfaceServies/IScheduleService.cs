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
        Task<IEnumerable<ScheduleViewModel>> GetAllSchedulesAsync();
        Task<ScheduleViewModel> CreateScheduleAsync(CreateScheduleViewModel createScheduleViewModel);
        Task<ScheduleViewModel> UpdateScheduleAsync(int id, UpdateScheduleViewModel updateScheduleViewModel);
        System.Threading.Tasks.Task DeleteScheduleAsync(int id);
        Task<IEnumerable<ScheduleViewModel>> GetSchedulesByUserIdAsync(string userId);
        Task<ScheduleViewModel> GetScheduleByIdAsync(int id);
        Task<ValidationResult> ValidateScheduleAsync(object model); // Adjust as needed for your validation logic
    }

}
