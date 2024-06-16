using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Application.IValidators;
using Application.ViewModels.FileViewModel;
using Application.ViewModels.GroupChatViewModel;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Models.File;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Services
{
    public class FileManageService : IFileManageService
    {
        private readonly IFileManageRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly IFileValidator _validator;

        public FileManageService(IFileManageRepository fileRepository, IMapper mapper, IFileValidator validator)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<Domain.Models.File> CreateFileAsync(FileViewModel fileViewModel)
        {
            var validationResult = await ValidateFileAsync(fileViewModel);
            if (!validationResult.IsValid)
            {
                var validationResults = validationResult.Errors
                    .Select(e => new System.ComponentModel.DataAnnotations.ValidationResult(e.ErrorMessage, new[] { e.PropertyName }))
                    .ToList();
                var validationMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException(validationMessage, validationResult.Errors);
            }

            var file = _mapper.Map<File>(fileViewModel);
            await _fileRepository.AddAsync(file);
            return file;
        }

        public async Task<Domain.Models.File> UpdateFileAsync(FileViewModel fileViewModel, int id)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            if (file == null)
                throw new KeyNotFoundException("File not found");

            var validationResult = await ValidateFileAsync(fileViewModel);
            if (!validationResult.IsValid)
            {
                var validationResults = validationResult.Errors
                    .Select(e => new System.ComponentModel.DataAnnotations.ValidationResult(e.ErrorMessage, new[] { e.PropertyName }))
                    .ToList();
                var validationMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException(validationMessage, validationResult.Errors);
            }

            _mapper.Map(fileViewModel, file);
            _fileRepository.Update(file);
            return file;
        }

        public async Task<FluentValidation.Results.ValidationResult> ValidateFileAsync(FileViewModel fileViewModel)
        {
            return await _validator.ValidateAsync(fileViewModel);
        }

        public async Task<IEnumerable<FileViewModel>> GetFileByTaskIdAsync(int taskId)
        {
            var files = await _fileRepository.GetFileByTaskIdAsync(taskId);
            return _mapper.Map<IEnumerable<FileViewModel>>(files);
        }

        public async Task<IEnumerable<FileViewModel>> GetFileByUserIdAsync(string userId)
        {
            var files = await _fileRepository.GetFileByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<FileViewModel>>(files);
        }

        public async Task<IEnumerable<FileViewModel>> GetFilesByUserIdAndTaskIdAsync(string userId, int taskId)
        {
            var files = await _fileRepository.GetAllAsync(query =>
                query.Where(file => file.UserId == userId && file.TaskId == taskId)
                     .Include(file => file.Users));

            return _mapper.Map<IEnumerable<FileViewModel>>(files);
        }

    }
}
