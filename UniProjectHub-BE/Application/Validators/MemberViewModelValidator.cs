using Application.ViewModels.MemberViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class MemberViewModelValidator : AbstractValidator<MemberViewModel>
    {
        public MemberViewModelValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.IsOwner).NotNull().WithMessage("IsOwner is required.");
         }
    }
}