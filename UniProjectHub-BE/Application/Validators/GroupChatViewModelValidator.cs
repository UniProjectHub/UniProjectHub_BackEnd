using Application.ViewModels.GroupChatViewModel;
using FluentValidation;

namespace Application.Validators
{
    public class GroupChatViewModelValidator : AbstractValidator<GroupChatViewModel>
    {
        public GroupChatViewModelValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.MemberId).NotEmpty();
            RuleFor(x => x.Messenger).NotEmpty();
            RuleFor(x => x.Status).InclusiveBetween(0, 2);
        }
    }
}
