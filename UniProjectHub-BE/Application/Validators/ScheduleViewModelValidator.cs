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
                .Must(BeAValidTime).WithMessage("Start time must be a valid time.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time is required.")
                .Must(BeAValidTime).WithMessage("End time must be a valid time.")
                .Must((model, endTime) => IsEndTimeGreaterThanStartTime(model.StartTime, endTime))
                .WithMessage("End time must be greater than start time.");

            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("Course name is required.");
        }

        private bool BeAValidTime(DateTime value)
        {
            // All DateTime values are valid so this method is not required
            return true;
        }

        private bool IsEndTimeGreaterThanStartTime(DateTime startTime, DateTime endTime)
        {
            return endTime > startTime;
        }
    }
}