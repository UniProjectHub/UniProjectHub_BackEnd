using Application.ViewModels.ScheduleViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateScheduleViewModelValidator : AbstractValidator<UpdateScheduleViewModel>
    {
        public UpdateScheduleViewModelValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("UserId is required.");

            RuleFor(x => x.DateOfWeek)
                .NotEmpty().WithMessage("DateOfWeek is required.")
                .Length(3, 10).WithMessage("DateOfWeek must be between 3 and 10 characters.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("StartTime is required.")
                .Matches(@"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$").WithMessage("Start time must be in HH:mm format.")
                .Must(BeAValidTime).WithMessage("Start time must be a valid time.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("EndTime is required.")
                .Matches(@"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$").WithMessage("End time must be in HH:mm format.")
                .Must(BeAValidTime).WithMessage("End time must be a valid time.")
                .Must((model, endTime) => IsEndTimeGreaterThanStartTime(model.StartTime, endTime))
                .WithMessage("End time must be greater than start time.");

            RuleFor(x => x.SlotStartTime)
                .NotEmpty().WithMessage("SlotStartTime is required.")
                .Matches(@"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$").WithMessage("SlotStartTime must be in HH:mm format.")
                .Must(BeAValidTime).WithMessage("SlotStartTime must be a valid time.");

            RuleFor(x => x.SlotEndTime)
                .NotEmpty().WithMessage("SlotEndTime is required.")
                .Matches(@"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$").WithMessage("SlotEndTime must be in HH:mm format.")
                .Must(BeAValidTime).WithMessage("SlotEndTime must be a valid time.")
                .Must((model, slotEndTime) => IsSlotEndTimeGreaterThanSlotStartTime(model.SlotStartTime, slotEndTime))
                .WithMessage("SlotEndTime must be greater than SlotStartTime.");

            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("CourseName is required.")
                .Length(3, 50).WithMessage("CourseName must be between 3 and 50 characters.");
        }

        private bool BeAValidTime(string value)
        {
            return TimeSpan.TryParse(value, out _);
        }

        private bool IsEndTimeGreaterThanStartTime(string startTime, string endTime)
        {
            if (TimeSpan.TryParse(startTime, out TimeSpan start) &&
                TimeSpan.TryParse(endTime, out TimeSpan end))
            {
                return end > start;
            }
            return false;
        }

        private bool IsSlotEndTimeGreaterThanSlotStartTime(string slotStartTime, string slotEndTime)
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