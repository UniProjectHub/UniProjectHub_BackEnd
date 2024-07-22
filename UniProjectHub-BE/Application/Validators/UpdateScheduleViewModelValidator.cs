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
                .NotEmpty().WithMessage("StartTime is required.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("EndTime is required.")
                .GreaterThan(x => x.StartTime).WithMessage("EndTime must be later than StartTime.");

            RuleFor(x => x.SlotStartTime)
                .NotEmpty().WithMessage("SlotStartTime is required.");

            RuleFor(x => x.SlotEndTime)
                .NotEmpty().WithMessage("SlotEndTime is required.")
                .GreaterThan(x => x.SlotStartTime).WithMessage("SlotEndTime must be later than SlotStartTime.");

            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("CourseName is required.")
                .Length(3, 50).WithMessage("CourseName must be between 3 and 50 characters.");
        }
    }
}