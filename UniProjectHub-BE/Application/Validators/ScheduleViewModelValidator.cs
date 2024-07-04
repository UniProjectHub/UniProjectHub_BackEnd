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
