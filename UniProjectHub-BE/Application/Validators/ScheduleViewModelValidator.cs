using FluentValidation;
using Application.ViewModels.ScheduleViewModel;

namespace Application.Validators
{
    public class ScheduleViewModelValidator : AbstractValidator<ScheduleViewModel>
    {
        public ScheduleViewModelValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.DateOfWeek)
                .NotEmpty().WithMessage("Date of week is required.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Start time is required.")
                .Must(BeAValidDateTime).WithMessage("Start time must be a valid date and time.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time is required.")
                .Must(BeAValidDateTime).WithMessage("End time must be a valid date and time.")
                .GreaterThan(x => x.StartTime).WithMessage("End time must be greater than start time.");

            RuleFor(x => x.SlotStartTime)
                .NotEmpty().WithMessage("Slot start time is required.")
                .Must(BeAValidDateTime).WithMessage("Slot start time must be a valid date and time.")
                .LessThan(x => x.StartTime).WithMessage("Slot start time must be less than start time.");

            RuleFor(x => x.SlotEndTime)
                .NotEmpty().WithMessage("Slot end time is required.")
                .Must(BeAValidDateTime).WithMessage("Slot end time must be a valid date and time.")
                .GreaterThan(x => x.EndTime).WithMessage("Slot end time must be greater than end time.");

            RuleFor(x => x.TeacherId)
                .NotEmpty().WithMessage("Teacher ID is required.");

            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("Course name is required.");
        }

        private bool BeAValidDateTime(DateTime value)
        {
            // You can add custom logic here to validate the DateTime format if necessary
            return true;
        }
    }
}
