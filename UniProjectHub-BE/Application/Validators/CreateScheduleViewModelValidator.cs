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
                .Must(BeAValidTime).WithMessage("StartTime must be a valid time.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("EndTime is required.")
                .Must(BeAValidTime).WithMessage("EndTime must be a valid time.")
                .Must((viewModel, endTime) => IsEndTimeValid(viewModel.StartTime, endTime))
                .WithMessage("EndTime must be later than StartTime.");

            RuleFor(x => x.SlotStartTime)
                .NotEmpty().WithMessage("SlotStartTime is required.")
                .Must(BeAValidTime).WithMessage("SlotStartTime must be a valid time.");

            RuleFor(x => x.SlotEndTime)
                .NotEmpty().WithMessage("SlotEndTime is required.")
                .Must(BeAValidTime).WithMessage("SlotEndTime must be a valid time.")
                .Must((viewModel, slotEndTime) => IsSlotEndTimeValid(viewModel.SlotStartTime, slotEndTime))
                .WithMessage("SlotEndTime must be later than SlotStartTime.");

            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("CourseName is required.")
                .Length(3, 50).WithMessage("CourseName must be between 3 and 50 characters.");
        }

        private bool BeAValidTime(DateTime value)
        {
            // All DateTime values are valid so this method is not required
            return true;
        }

        private bool IsEndTimeValid(DateTime startTime, DateTime endTime)
        {
            return endTime > startTime;
        }

        private bool IsSlotEndTimeValid(DateTime slotStartTime, DateTime slotEndTime)
        {
            return slotEndTime > slotStartTime;
        }
    }
}