﻿using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Domain.Models;
using AutoMapper;
using Application.ViewModels.GroupChatViewModel;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services
{
    public class GroupChatService : IGroupChatService
    {
        private readonly IGroupChatRepository _groupChatRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GroupChatViewModel> _validator;

        public GroupChatService(IGroupChatRepository groupChatRepository, IMapper mapper, IValidator<GroupChatViewModel> validator)
        {
            _groupChatRepository = groupChatRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ValidationResult> ValidateGroupChatAsync(GroupChatViewModel groupChatViewModel)
        {
            return await _validator.ValidateAsync(groupChatViewModel);
        }

        public async Task<GroupChatViewModel> CreateGroupChatAsync(GroupChatViewModel groupChatViewModel)
        {
            var validationResult = await ValidateGroupChatAsync(groupChatViewModel);
            if (!validationResult.IsValid)
            {
                throw new ValidationException("Validation failed", validationResult.Errors);
            }

            var groupChat = _mapper.Map<GroupChat>(groupChatViewModel);
            await _groupChatRepository.AddAsync(groupChat);
            return _mapper.Map<GroupChatViewModel>(groupChat);
        }

        public async Task<GroupChatViewModel> UpdateGroupChatAsync(GroupChatViewModel groupChatViewModel, int id)
        {
            var groupChat = await _groupChatRepository.GetByIdAsync(id);
            if (groupChat == null)
                throw new KeyNotFoundException("Group chat not found");

            var validationResult = await ValidateGroupChatAsync(groupChatViewModel);
            if (!validationResult.IsValid)
            {
                throw new ValidationException("Validation failed", validationResult.Errors);
            }

            _mapper.Map(groupChatViewModel, groupChat);
            await _groupChatRepository.UpdateAsync(groupChat);
            return _mapper.Map<GroupChatViewModel>(groupChat);
        }

        public async Task<IEnumerable<GroupChatViewModel>> GetGroupChatsByProjectIdAsync(int projectId)
        {
            var groupChats = await _groupChatRepository.GetGroupChatsByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<GroupChatViewModel>>(groupChats);
        }

        public async Task<List<GroupChatViewModel>> GetAllGroupChatsAsync()
        {
            var groupChats = await _groupChatRepository.GetAllAsync();
            return _mapper.Map<List<GroupChatViewModel>>(groupChats);
        }

        public async Task<GroupChatViewModel> GetGroupChatByIdAsync(int id)
        {
            var groupChat = await _groupChatRepository.GetByIdAsync(id);
            return _mapper.Map<GroupChatViewModel>(groupChat);
        }

        public async System.Threading.Tasks.Task DeleteGroupChatAsync(int id)
        {
            await _groupChatRepository.DeleteAsync(id);
        }
    }
}