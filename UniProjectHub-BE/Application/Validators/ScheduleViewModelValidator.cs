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
                .Matches(@"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$").WithMessage("Start time must be in HH:mm format.")
                .Must(BeAValidTime).WithMessage("Start time must be a valid time.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time is required.")
                .Matches(@"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$").WithMessage("End time must be in HH:mm format.")
                .Must(BeAValidTime).WithMessage("End time must be a valid time.")
                .Must((model, endTime) => IsEndTimeGreaterThanStartTime(model.StartTime, endTime))
                .WithMessage("End time must be greater than start time.");

            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("Course name is required.");
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
    }
}