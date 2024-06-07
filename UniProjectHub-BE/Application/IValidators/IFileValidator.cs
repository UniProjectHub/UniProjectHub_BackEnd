using Application.ViewModels.FileViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IValidators
{
    public interface IFileValidator : IValidator<FileViewModel>
    {
        
    }
}
