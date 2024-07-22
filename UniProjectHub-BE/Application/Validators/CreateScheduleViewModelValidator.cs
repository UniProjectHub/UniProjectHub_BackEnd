using Application.ViewModels.ScheduleViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateScheduleViewModelValidator : AbstractValidator<CreateScheduleViewModel>
    {
        public CreateScheduleViewModelValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("UserId is required.");

            RuleFor(x => x.DateOfWeek)
                .NotEmpty().WithMessage("DateOfWeek is required.")
                .Length(3, 10).WithMessage("DateOfWeek must be between 3 and 10 characters.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("StartTime is required.")
                .Matches(@"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$").WithMessage("StartTime must be a valid time in HH:mm format.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("EndTime is required.")
                .Matches(@"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$").WithMessage("EndTime must be a valid time in HH:mm format.")
                .Must((viewModel, endTime) => IsEndTimeValid(viewModel.StartTime, endTime))
                .WithMessage("EndTime must be later than StartTime.");

            RuleFor(x => x.SlotStartTime)
                .NotEmpty().WithMessage("SlotStartTime is required.")
                .Matches(@"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$").WithMessage("SlotStartTime must be a valid time in HH:mm format.");

            RuleFor(x => x.SlotEndTime)
                .NotEmpty().WithMessage("SlotEndTime is required.")
                .Matches(@"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$").WithMessage("SlotEndTime must be a valid time in HH:mm format.")
                .Must((viewModel, slotEndTime) => IsSlotEndTimeValid(viewModel.SlotStartTime, slotEndTime))
                .WithMessage("SlotEndTime must be later than SlotStartTime.");

            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("CourseName is required.")
                .Length(3, 50).WithMessage("CourseName must be between 3 and 50 characters.");
        }

        private bool IsEndTimeValid(string startTime, string endTime)
        {
            if (TimeSpan.TryParse(startTime, out TimeSpan start) &&
                TimeSpan.TryParse(endTime, out TimeSpan end))
            {
                return end > start;
            }
            return false;
        }

        private bool IsSlotEndTimeValid(string slotStartTime, string slotEndTime)
        {
            if (TimeSpan.TryParse(slotStartTime, out TimeSpan slotStart) &&
                TimeSpan.TryParse(slotEndTime, out TimeSpan slotEnd))
            {
                return slotEnd > slotStart;
            }
            return false;
        }
    }
}