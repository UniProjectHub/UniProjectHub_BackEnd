using Application.IValidators;
using Application.ViewModels.FileViewModel;
using Application.ViewModels.GroupChatViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class FileViewModelValidator : AbstractValidator<FileViewModel>, IFileValidator
    {
        public FileViewModelValidator()
        {
            RuleFor(x => x.Filename).NotEmpty().WithMessage("File name must not be empty");
            // Add more validation rules as needed
        }
    }
}
