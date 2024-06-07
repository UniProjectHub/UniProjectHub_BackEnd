using Application.ViewModels.FileViewModel;
using Application.ViewModels.GroupChatViewModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface IFileManageService
    {
        Task<FluentValidation.Results.ValidationResult> ValidateFileAsync(FileViewModel fileViewModel);
        Task<Domain.Models.File> CreateFileAsync(FileViewModel fileViewModel);
        Task<Domain.Models.File> UpdateFileAsync(FileViewModel fileViewModel, int id);
        // Task DeleteGroupChatAsync(int id);
        Task<IEnumerable<FileViewModel>> GetFileByTaskIdAsync(int taskId);
        Task<IEnumerable<FileViewModel>> GetFileByUserIdAsync(int userId);
    }
}
