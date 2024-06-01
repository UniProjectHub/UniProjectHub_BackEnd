using Application.ViewModels.GroupChatViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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