using Application.ViewModels.FileViewModel;
using Application.ViewModels.GroupChatViewModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Models.File;

namespace Application.InterfaceServies
{
    public interface IFileManageService
    {
        Task<FluentValidation.Results.ValidationResult> ValidateFileAsync(FileViewModel fileViewModel);
        Task<File> CreateFileAsync(FileViewModel fileViewModel);
        Task<File> UpdateFileAsync(FileViewModel fileViewModel, int id);
        // Task DeleteGroupChatAsync(int id);
        Task<IEnumerable<FileViewModel>> GetFileByTaskIdAsync(int taskId);
        Task<IEnumerable<FileViewModel>> GetFileByUserIdAsync(string userId);
        Task<IEnumerable<FileViewModel>> GetFilesByUserIdAndTaskIdAsync(string userId, int taskId);
        Task<bool> IsDuplicateFileAsync(int taskId, string fileName);
    }
}
